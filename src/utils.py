def parseUser(name, password, userConfig="./users.txt"):
    userCategories = dict()
    try:
        # Открываем файл и читаем первую строку.
        file = open(userConfig, 'r', encoding='UTF-8')
        line = file.readline()

        # Продолжаем читать по строке, пока не дойдем до конца файла.
        while line:
            if line[0] == '#':
                userData = line[1::].split()
                if name == userData[0] and password == userData[1]:
                    categoryLine = file.readline()
                    while categoryLine:
                        if categoryLine[0] == '#' or categoryLine[0] == '\n':
                            return {'status': 'accepted', 'data': userCategories}

                        categoryParams = categoryLine.replace('\n', '').split()

                        if len(categoryParams) < 2 or not categoryParams[-1].isdigit():
                            raise KeyError

                        userCategories.update({" ".join(categoryParams[0:len(categoryParams) - 1]): categoryParams[-1]})

                        categoryLine = file.readline()
                    return {'status': 'accepted', 'data': userCategories}

            line = file.readline()
        file.close()
    except FileExistsError:
        print('File does not exist')
    return {'status': 'denied'}
