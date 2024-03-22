from tkinter import *
from tkinter import messagebox


# Главный класс, отрисовывающий окно с меню.
class MenuWindow:
    # Словарь из лямбда выражений, которые отрабатывают при нажатии на определенные разделы меню.
    functions = {
        'Others': lambda: messagebox.showinfo(message='Разное'),
        'Stuff': lambda: messagebox.showinfo(message='Сотрудники'),
        'Orders': lambda: messagebox.showinfo(message='Приказы'),
        'Docs': lambda: messagebox.showinfo(message='Документы'),
        'Departs': lambda: messagebox.showinfo(message='Отделы'),
        'Towns': lambda: messagebox.showinfo(message='Города'),
        'Posts': lambda: messagebox.showinfo(message='Должности'),
        'Window': lambda: messagebox.showinfo(message='Окно'),
        'Content': lambda: messagebox.showinfo(message='Оглавление'),
        'About': lambda: messagebox.showinfo(message='О программе')
    }

    # Метод, который составляет матрицу из разделов меню, хранящихся в файле.
    def parseMenu(self):

        # Список, в котором будут категории меню. Из него и будет сформированно в будущем менею приложения.
        menuData = []

        # Открывавем файл с конфигурацией меню.
        try:
            file = open(self.fileName, 'r', encoding='UTF-8')
        except FileExistsError:
            messagebox.showerror(title='Ошибка', message='Файла конфигурации не существует!')
            return []

        # Открытый файл представляет собой конфиг, где каждая строчка - это настройка раздела меню.
        # <Уровень вложенности раздела: int> <Название раздела: str> <Модификатор доступа: int> <Название функции: str>

        # Читаем открытый файл построчно.
        line = file.readline().strip()
        while line:

            # Список из элементов текущей строки, разбитой по пробелам.
            listOfLineItems = line.split()

            # Список, в который мы сложим текущую категорию/раздел меню.
            currentCategory = []

            # Проверяем, что первйы элемент строки - это число и добавляем его в текущую категорию, если это так.
            # Первый элемент обязательно должен быть числом, т.к. это уровень вложенности категории меню.
            if listOfLineItems[0].isdigit():
                currentCategory.append(int(listOfLineItems[0]))
            else:
                messagebox.showerror(title='Ошибка', message='Ошибка в файле конфигурации меню!')
                return []

            # Создаем временную строку, в которую будут сконкатенированы все элементы до следующего числа в строке.
            # Т.е. до модификатора доступа к разделу.
            temp = ''

            # В этой переменной запоминаем индекс последнего элемента, который является строкой.
            lastStringIndex = 0

            # Идем в цикле по списку из элементов текущей строки файла, разделенной по пробелам.
            for index in range(1, len(listOfLineItems)):
                # Если следующий элемент - это число.
                if listOfLineItems[index + 1].isdigit():
                    # То перезаписываем временную строку таким образом, чтобы все элементы между двумя числами.
                    # В текущей строке файла были объеденены в одну строку, формруюя название раздела.
                    # Это необходимо для того случая, когда название раздела состоит из 2-х и более слов.

                    # Перезаписываем временную строку (в ней будет название раздела. Потенциально с пробелами).
                    temp = temp + ' ' + listOfLineItems[index]
                    # Запоминаем индекс последнего элемента перед числом.
                    lastStringIndex = index
                    # Записываем в текущую категорию название. И выходим из цикла.
                    currentCategory.append(temp.strip())
                    break
                # Иначе перезаписываем временную строку, пока не войдем в условие выше. И запоминаем индекс.
                else:
                    temp = temp + ' ' + listOfLineItems[index]
                    lastStringIndex = index
            # В этом месте у нас уже в currentCategory записаны уровень вложенности текущей категории и её название.

            # Этой строчкой мы дописываем в текущую категорию модификатор доступа и название функции.
            # Набор функций представлен лямбда выражениями в словаре functions этого класса.
            currentCategory = currentCategory + listOfLineItems[lastStringIndex + 1::]

            # Проверка корректности конфига текущей категории.
            if (len(currentCategory) < 3) or (len(currentCategory) > 4):
                messagebox.showerror(title='Ошибка', message='Ошибка в файле конфигурации меню!')
                return []

            # Переводим числа из типа str в тип int для удобства при формировании меню.
            currentCategory[2] = int(currentCategory[2])

            # Здесь подставляем модификатор доступа для конкретной категории у конкретного пользователя.
            # Если у пользователя в его конфиге прописан модификатор доступа к текущей категории, то мы его подставим.
            if self.userData.get(currentCategory[1]):
                currentCategory[2] = int(self.userData[currentCategory[1]])

            # Добавляем собранную категорию в список с категориями.
            menuData.append(currentCategory)
            # Читаем следующую строчку файла.
            line = file.readline()

        return menuData

    # Функция, которая устанавливает меню. Принимает в себя объект Menu и список списков, который представляет собой конфиг меню.
    def setMenu(self, menu, config: list[list]):
        # Текущий уровень меню.
        currentLevel = config[0][0]
        i = 0

        # Идем до конца конфига от текущего элемента.
        while i < len(config):
            # Если модификатор доступа у категории равен 2, то эту категорию не добавляем в меню.
            # Пользователь её не видит.
            if config[i][2] == 2:

                # Сохраняем в перменную cur текущую категорию.
                cur = config[i][0]
                i += 1

                # Идем далее по конфигу, и игнорируем все категории, которые являются вложенными для текущей.
                # Их пользователь тоже не будет видеть.
                while i < len(config) and config[i][0] != cur:
                    i += 1
                continue

            # Проверяем корректность конфига для текущей категории.
            # Если у категории есть подкатегории, то родительская категория не может содержать вызов функции при нажатии.
            if i < len(config) - 1 and len(config[i]) == 4 and config[i + 1][0] > config[i][0]:
                messagebox.showerror(message=f"Заголовок подменю \"{config[i][1]}\" содержит вызов функции")
                i += 1
                # Игнорируем все подкатегории в случае, если категория содержит и функцию и подкатегории.
                while i < len(config) and config[i][0] != currentLevel:
                    i += 1
                continue

            # Если категория не содержит подкатегорий, то мы добавляем её в меню, с учетом модификатора доступа.
            if len(config[i]) == 4:
                try:
                    # Переменная, которая содержит ссылку на лямбда выражение из словаря.
                    fn = self.functions[config[i][-1]]

                    # Добавляем в меню новый раздел.
                    menu.add_command(label=config[i][1],
                                     state=(('normal', 'disabled')[config[i][2]]),
                                     command=fn
                                     )
                except KeyError:
                    messagebox.showerror(message=f"У элемента {config[i][1]} не создана функция")
                i += 1
                continue

            # Проверка, прописано ли подменю у категории, которя не имеет функции.
            if len(config[i]) == 3 and config[i + 1][0] <= currentLevel:
                messagebox.showerror(message=f"У элемента {config[i][1]} не создано подменю")
                i += 1
                continue

            # В случае, если у категории есть подкатегории.
            else:
                # Переменная - подменю.
                submenu = Menu(tearoff=0)
                k = i + 1
                # Идем до момента, пока не найдем категорию меню не равную по уровню вложенности с текущей.
                while (k < len(config)) and (config[k][0] != currentLevel):
                    k += 1

                # Собираем подменю. Т.е. все элементы, до следующей категории, равной по уровню вложенности с текущей.
                subconfig = [config[i] for i in range(i + 1, k)]

                # Если подменю есть, то добавляем к меню текущую категорию, и вызываем функцию рекурентно, для заполения подменю.
                # Теоретически это позволяет делать подменю любого уровня вложенности.
                if subconfig:
                    menu.add_cascade(label=config[i][1], menu=self.setMenu(submenu, subconfig),
                                     state=('normal', 'disabled')[config[i][2]])

                i = k

        return menu

    # В конструктор подается объект userData представляющий собой словарь, содержащий ключ data.
    # UserData['data'] - словарь, содержащий пары: название раздела, модификатор доступа к разделу.
    # fileName - путь к файлу с конфигурацией меню.
    def __init__(self, userData, fileName="./menu.txt"):
        self.fileName = fileName
        self.userData = userData['data']
        print(userData)

        # Создание нового окна средствами Tkinter, в котором и будет конфигурируемое меню.
        self.window = Tk()
        self.window.geometry('550x350')
        self.window.title("Menu")
        self.window.configure(bg='#b9d1ea')
        self.window.minsize(550, 350)
        self.window.resizable(False, False)

        # Создание и добавление меню в окно.
        menu = self.setMenu(Menu(), self.parseMenu())
        self.window.config(menu=menu)
        self.window.mainloop()
