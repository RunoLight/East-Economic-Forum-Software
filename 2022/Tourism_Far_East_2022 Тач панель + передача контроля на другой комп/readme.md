Unity version: 2020.3.35f1
---
Pedendencies for running build:
---
Dotnet runtime 6.0.7 x64

Dotnet sdk 6.0.302 x64

Windows desktop runtime 3.1.27 win x64

ПО на компьютере с проекторами:
Resolution


Настройка:
-----
0 Оба компьютера подключить друг к другу через Ethernet-кабель

1 Компьютер с тач-панелью:

ВАЖНО: Этапы 1.1-2.4 нужно выполнять если видео на компе с проекторами не запускаются (Например если сменилась сеть интернета или его настройки)

1.1 Открыть консоль, вписать "ipconfig"
1.2 Найти "Ethernet adapter Ethernet:"
1.3.1 Ниже найти "IPv4 Address : *.*.*.*" (Вместо * будут цифры, например: 169.254.28.222).
1.3.2 Если Адрес IPv4 отсутсвует, Ethernet adapter не находится: Переподключить кабель, посмотреть IPv4 адрес через:
	Панель управления\Сеть и Интернет\Центр управления сетями и общим доступом\Сеть с именем Ethernet\Сведения\IPv4
1.4 Пишем в консоль "arp -a"
1.5 Ищем Interface с найденным ранее IPv4 Address, берем оттуда ip с припиской dynamic
1.6 Пишем в консоль "ping <найденный ip>"
1.7 Так среди dynamic адресов ищем тот который будет отвечать (У которого в ответе потери = 0 пакетов)

2.1 ТОЛЬКО ПРИ ПЕРВОЙ УСТАНОВКЕ (УЖЕ СДЕЛАНО) - Открыть папку "Installation", установить windowsdesktop-runtime, dotnet-runtime.
2.2 Открыть папку "TouchPanel Computer"
2.3 Открыть конфиг "ServerData.config"
2.4 Вписать вместо существующего в конфиге адреса - Адрес, полученный на прошлом этапе.
2.5 Запустить файл "FE-Tourism.exe"

3 Компьютер с проекторами:
3.1 ТОЛЬКО ПРИ ПЕРВОЙ УСТАНОВКЕ (УЖЕ СДЕЛАНО) - Открыть папку "Installation", установить dotnet-sdk, dotnet-runtime.
3.2 Открыть папку "Projector Computer"
3.3 Запустить файл "RUN.bat"
Это запустит сервер. НЕ ЗАКРЫВАЙТЕ КОНСОЛЬ, это закроет сервер!

Если что-то не работает:
-----

1 Компьютер с проекторами:
Войти в браузер, по адресу http://localhost:5071/
Должно выдать "Everything is fine".
Если выдает 404/can't reach page:
Закрыть консоль с сервером, запустить снова используя команды:
	dotnet restore
	dotnet run --urls http://*:5071

2 Компьютер с тач-панелью:
Войти в браузер, по адресу http://Адрес-полученный-ранее:5071/
Должно выдать "Everything is fine".
Если выдает 404/can't reach page:
	Выключить фаерволл/защитник Windows на обоих компьютерах
