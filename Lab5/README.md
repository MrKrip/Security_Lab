# Security 5-6

# 5 лаболаторна
_______________

## Як усе тут влаштовано

Проект написаний на мові C# із застосунком схеми MVC(Model-view-controller) та також висористовували базу данних sqlSrever. проект має лише 2 сторінки(реєстрація та логування)

## Подробитці ховання пароля
Код для хешування пароля був використаний із попередніх лаборатних. Спочатку пароль хешується за допомогою алгоритму MD5. Далі хеш шифрується за домопомого AesGcm.Після цього хешується за допомогою алгоритму Bcrypt.
Ключ для неї береться із заданої змінної, nonce генерується випадковим чином. nonce генерується із абстрактного класу, від якого успадковуються всі реалізації криптографічних генераторів випадкових чисел із модуля  System.Security.Cryptography. 
Bcrypt був обранний через те що він розроблений для того, щоб бути дорогим в обчислювальному відношенні, щоб спроба злому пароля займала ще більше часу. Та був додадний MD5 для більшої надійності.

________________________________________________________________________________________________

# 6 лаболаторна
_______________

У цій лабораторній роботі ми шифруємо:
1 публічну змінну phoneNumber
1 приватну змінну Creditcard numвer

Дані шифруються за допомогою алгоритму AES GSM із заданим ключем та випадковим , nonce, як раніше згадувалося, генерується випадковим чином. nonce генерується із абстрактного класу, від якого успадковуються всі реалізації криптографічних генераторів випадкових чисел із модуля  System.Security.Cryptography.Ключ для неї береться із заданої змінної.

При вході на сайт користувачеві будуть надаватися всі імейли зареєстрованих користувачів, а також їх номери телефонів, які ми утримуємо зашифрованими в базі.

Для цієї роботи були написані функції шифрування та дишифрування виконуються у класі AES_GCM. На вхід обидва методи приймають (chipertext/plaintext, _config, nonce, tag)
