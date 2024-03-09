import tkinter.messagebox
from tkinter import *
from utils import parseUser
import win32api
from PIL import Image, ImageTk
from menuWindow import MenuWindow
import tomllib

window = Tk()
window.geometry('550x350')
window.title("Вход")
window.configure(bg='#b9d1ea')
window.minsize(550, 350)
window.resizable(False, False)

# ------- welcome label region --------------
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

# -------------------------------------------

# ------------ version label region ---------
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

# -------------------------------------------

# -------------login form message region --------------------
formMessage = Label(window,
                    text="Введите имя пользователя и пароль",
                    background="white",
                    foreground="black",
                    font=20,
                    anchor="e")

formMessage.pack(fill="x",
                 padx=5,
                 anchor='n')

# -----------------------------------------------------------


# ------------ keyboard language layout message region ---------
languageMessage = Label(window,
                        background=window['bg'],
                        font=12)
languageMessage.pack(anchor="sw",
                     side=BOTTOM,
                     pady=0)


def checkKeyBoardLayout():
    keyboardLayoutKey = win32api.GetKeyboardLayout()
    if keyboardLayoutKey == 68748313:
        languageMessage.config(text="Язык ввода Русский")
    elif keyboardLayoutKey == 67699721:
        languageMessage.config(text="Язык ввода Английский")

    window.after(100, checkKeyBoardLayout)


# --------------------------------------------------------------

# ------------ caps lock message region ---------
capsLockMessage = Label(window,
                        background=window['bg'],
                        font=12)
capsLockMessage.pack(anchor="se",
                     side=BOTTOM)

capsLockMessage.place(relx=0.54,
                      rely=0.92)


def checkCapsLock():
    if win32api.GetKeyState(0x14) in (1, -127):
        capsLockMessage.config(text="Клавиша CapsLock нажата")
    else:
        capsLockMessage.config(text="")
    window.after(100, checkCapsLock)


# -----------------------------------------------


# ------------ login field region ---------------
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

# -----------------------------------------------

# ------------ password field region ---------------
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


# -----------------------------------------------


# ------------ logIn button region ---------------
def tryLogIn():
    username = userNameInputField.get().strip()
    password = passwordInputField.get().strip()

    try:
        userData = parseUser(name=username, password=password)
    except KeyError:
        tkinter.messagebox.showerror(message="Некорректные настройки доступа")
        return

    if userData['status'] == 'accepted':
        quit()
        MenuWindow(userData)
    else:
        tkinter.messagebox.showerror(title='Ошибка', message='Отказанно в доступе\n'
                                                             'Неверное имя пользователя или пароль!')


logInButton = Button(window,
                     text="Вход",
                     font=3,
                     width=15,
                     command=tryLogIn)
logInButton.place(x=35, y=250)

# -----------------------------------------------


# ------------ cancel button region ---------------
cancelButton = Button(window,
                      text="Отмена",
                      font=3,
                      width=15,
                      command=lambda: window.destroy())
cancelButton.place(x=370, y=250)

# -----------------------------------------------

# ------- logo image region ----------------------
image = Image.open("./logo.png")
photo = ImageTk.PhotoImage(image, 13)
logo = Label(window,
             image=photo,
             background=window['bg'],
             width=87,
             height=70)
logo.place(x=10, y=5)


# ------------------------------------------

def quit():
    for after_id in window.tk.eval('after info').split():
        window.after_cancel(after_id)
    window.destroy()


# Точка входа - запуск окна входа.
def start():
    checkKeyBoardLayout()
    checkCapsLock()

    window.protocol('WM_DELETE_WINDOW', quit)
    window.mainloop()
