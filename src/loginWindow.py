import tkinter.messagebox
from tkinter import *
from utils import parseUser
import win32api
from PIL import Image, ImageTk
from menuWindow import MenuWindow
import tomllib

# Создание окна средствами Tkinter.
window = Tk()
window.geometry('550x350')
window.title("Вход")
window.configure(bg='#b9d1ea')
window.minsize(550, 350)
window.resizable(False, False)

# Надпись - название приложения.
welcome = Label(window,
                text="АИС Отдел Кадров",
                background="#fffacd",
                foreground="black",
                font=20,
                anchor="e")

welcome.pack(fill="x",
             padx=5,
             pady=5,
             anchor='n')

# Надпись, информирующая о версии приложения.
version = Label(window,
                text='Версия ' + tomllib.load(open('pyproject.toml', 'rb'))['project']['version'],
                background="#ffd700",
                foreground="black",
                font=20,
                anchor="e")

version.pack(fill="x",
             padx=5,
             pady=(0, 5),
             anchor='n')

# Надпись, предлагающая ввести логин и пароль.
formMessage = Label(window,
                    text="Введите имя пользователя и пароль",
                    background="white",
                    foreground="black",
                    font=20,
                    anchor="e")

formMessage.pack(fill="x",
                 padx=5,
                 anchor='n')

# Надпись на форме, информирущая о языке раскладки.
languageMessage = Label(window,
                        background=window['bg'],
                        font=12)
languageMessage.pack(anchor="sw",
                     side=BOTTOM,
                     pady=0)


# Функция, которая каждые 100 милисекунд проверяет раскладку клавиатуры.
def checkKeyBoardLayout():
    keyboardLayoutKey = win32api.GetKeyboardLayout()
    if keyboardLayoutKey == 68748313:
        languageMessage.config(text="Язык ввода Русский")
    elif keyboardLayoutKey == 67699721:
        languageMessage.config(text="Язык ввода Английский")

    window.after(100, checkKeyBoardLayout)


# Надпись на форме, информирующая о Caps Lock.
capsLockMessage = Label(window,
                        background=window['bg'],
                        font=12)
capsLockMessage.pack(anchor="se",
                     side=BOTTOM)

capsLockMessage.place(relx=0.54,
                      rely=0.92)


# Функция, которая каждые 100 милисекунд проверяет нажат ли Caps Lock.
def checkCapsLock():
    if win32api.GetKeyState(0x14) in (1, -127):
        capsLockMessage.config(text="Клавиша CapsLock нажата")
    else:
        capsLockMessage.config(text="")
    window.after(100, checkCapsLock)


# Строка логина.
userNameDescription = Label(window,
                            text="Имя пользователя",
                            font=14,
                            background=window['bg'])
userNameDescription.place(x=5, y=125)

userNameInputField = Entry(window,
                           font=14,
                           width=30)

userNameInputField.pack(pady=25,
                        padx=5,
                        anchor="e")

# Строка пароля.
passwordDescription = Label(window,
                            text="Пароль",
                            font=14,
                            background=window['bg'])
passwordDescription.place(x=5, y=180)

passwordInputField = Entry(window,
                           font=14,
                           width=30,
                           show="*")

passwordInputField.pack(pady=0,
                        padx=5,
                        anchor="e")


# Обработчик нажатия на кнопку входа.
def tryLogIn():
    # Парсим данные с формы.
    username = userNameInputField.get().strip()
    password = passwordInputField.get().strip()

    # Проверяем есть ли пользователь в базе.
    try:
        userData = parseUser(name=username, password=password)
    except KeyError:
        tkinter.messagebox.showerror(message="Некорректные настройки доступа")
        return

    # Если есть, то ставим статус accepted.
    if userData['status'] == 'accepted':
        # Уничтожаем окно входа.
        quit()
        # Запускаем новое окно с меню.
        MenuWindow(userData)
    else:
        tkinter.messagebox.showerror(title='Ошибка', message='Отказанно в доступе\n'
                                                             'Неверное имя пользователя или пароль!')


# Кнопка входа.
logInButton = Button(window,
                     text="Вход",
                     font=3,
                     width=15,
                     command=tryLogIn)
logInButton.place(x=35, y=250)

# Кнопка отмены.
cancelButton = Button(window,
                      text="Отмена",
                      font=3,
                      width=15,
                      command=lambda: window.destroy())
cancelButton.place(x=370, y=250)

# Логотип - ключики.
image = Image.open("./logo.png")
photo = ImageTk.PhotoImage(image, 13)
logo = Label(window,
             image=photo,
             background=window['bg'],
             width=87,
             height=70)
logo.place(x=10, y=5)


# Функция, которая убивает все процессы, отслеживающие изменения в окне и закрывает окно.
# Такие как сменя языка и нажатие на клавишу Caps Lock.
def quit():
    # Идем в цикле, пока не завершим исполнение всех процессов.
    for after_id in window.tk.eval('after info').split():
        window.after_cancel(after_id)
    # Затем убиваем окно.
    window.destroy()


# Точка входа - запуск окна входа.
def start():
    # Запускаем процессы, которые будут следить за изменением состояния клавишы Caps Lock и языком раскладки.
    checkKeyBoardLayout()
    checkCapsLock()

    # Подвязываем функцию quit, как убивающую окно входа.
    window.protocol('WM_DELETE_WINDOW', quit)

    # Запускаем окно входа.
    window.mainloop()
