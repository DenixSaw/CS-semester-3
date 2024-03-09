from tkinter import *
from tkinter import messagebox


# Главный класс, отрисовывающий окно с меню.
class MenuWindow:
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
        menuData = []

        try:
            file = open(self.fileName, 'r', encoding='UTF-8')
        except FileExistsError:
            messagebox.showerror(title='Ошибка', message='Файла конфигурации не существует!')
            return []

        line = file.readline().strip()
        while line:

            listOfLineItems = line.split()
            currentCategory = []

            if listOfLineItems[0].isdigit():
                currentCategory.append(int(listOfLineItems[0]))
            else:
                messagebox.showerror(title='Ошибка', message='Ошибка в файле конфигурации меню!')
                return []

            temp = ''
            lastStringIndex = 0

            for index in range(1, len(listOfLineItems)):
                if listOfLineItems[index + 1].isdigit():
                    temp = temp + ' ' + listOfLineItems[index]
                    lastStringIndex = index
                    currentCategory.append(temp.strip())
                    break
                else:
                    temp = temp + ' ' + listOfLineItems[index]
                    lastStringIndex = index
            currentCategory = currentCategory + listOfLineItems[lastStringIndex + 1::]

            if (len(currentCategory) < 3) or (len(currentCategory) > 4):
                messagebox.showerror(title='Ошибка', message='Ошибка в файле конфигурации меню!')
                return []

            currentCategory[2] = int(currentCategory[2])

            if self.userData.get(currentCategory[1]):
                currentCategory[2] = int(self.userData[currentCategory[1]])

            menuData.append(currentCategory)
            line = file.readline()

        return menuData

    def setMenu(self, menu, config):
        currentLevel = config[0][0]
        i = 0

        while i < len(config):
            if config[i][2] == 2:
                cur = config[i][0]
                i += 1
                while i < len(config) and config[i][0] != cur:
                    i += 1
                continue

            if i < len(config) - 1 and len(config[i]) == 4 and config[i + 1][0] > config[i][0]:
                messagebox.showerror(message=f"Заголовок подменю \"{config[i][1]}\" содержит вызов функции")
                i += 1
                while i < len(config) and config[i][0] != currentLevel:
                    i += 1
                continue

            if len(config[i]) == 4:
                try:
                    fn = self.functions[config[i][-1]]
                    menu.add_command(label=config[i][1],
                                     state=(('normal', 'disabled')[config[i][2]]),
                                     command=fn
                                     )
                except KeyError:
                    messagebox.showerror(message=f"У элемента {config[i][1]} не создана функция")
                i += 1
                continue

            if len(config[i]) == 3 and config[i + 1][0] <= currentLevel:
                messagebox.showerror(message=f"У элемента {config[i][1]} не создано подменю")
                i += 1
                continue

            else:
                submenu = Menu(tearoff=0)
                k = i + 1

                while (k < len(config)) and (config[k][0] != currentLevel):
                    k += 1

                subconfig = [config[i] for i in range(i + 1, k)]
                if subconfig:
                    menu.add_cascade(label=config[i][1], menu=self.setMenu(submenu, subconfig), state=('normal', 'disabled')[config[i][2]])

                i = k

        return menu

    def __init__(self, userData, fileName="./menu.txt"):
        self.fileName = fileName
        self.userData = userData['data']

        self.window = Tk()
        self.window.geometry('550x350')
        self.window.title("Menu")
        self.window.configure(bg='#b9d1ea')
        self.window.minsize(550, 350)
        self.window.resizable(False, False)

        menu = self.setMenu(Menu(), self.parseMenu())
        self.window.config(menu=menu)
        self.window.mainloop()
