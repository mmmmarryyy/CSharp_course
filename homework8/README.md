# Лабораторная 8. Многопоточность I

## Задание 1. Dead lock (2 балла) - done

Создайте консольное приложение, в котором гарантированно возникает мертвая блокировка. Годится любой пример.

## Задание 2. Синхронизованный вывод (3 балла) - done

Пусть есть два потока T1 и T2, каждый из которых выводит на экран по 10 строк текста.

Предложите решение, в котором вывод потоков будет синхронизован: сначала поток T1 выводит первую строку, затем T2, затем T1 вторую строку и т.д. Используйте примитивы синхронизации: семафоры, мьютексы или др.

## Задание 3. Вывод FooBar (4 балла) - done
 
Предположим, вам дан следующий код:
```c#
public class FooBar {
    private int n;
    
    public FooBar(int n) {
        this.n = n; 
    }

    public void Foo(Action printFoo) { 
        for (int i = 0; i < n; i++) {
            printFoo(); 
        }
    }

    public void Bar(Action printBar) {
        for (int i = 0; i < n; i++) {
            printBar(); 
        }
    } 
}
```

Один и тот же экземпляр класса FooBar передается в два разных потока: поток A вызывает метод Foo(), а поток B вызывает Bar().

Напишите программу так, чтобы она выводила «foobar» n раз.

**Менять что-либо в классе FooBar нельзя!**

### Пример 1:
Ввод: n = 1  
Вывод: "foobar"  
Объяснение: Два потока запускаются асинхронно. Один из них вызывает Foo(), а другой - Bar ().  
"foobar" выводится 1 раз.  

### Пример 2:
Ввод: n = 2  
Вывод: "foobarfoobar"  
Объяснение: "foobar" выводится 2 раза.  

## Задание 4. Верна ли судоку (4 балла) - done

Определите, верна ли судоку размером 9 x 9. Заполненные ячейки должны быть проверены в соответствии со следующими правилами:
- Каждая строка должна содержать цифры от 1 до 9 без повторения.
- Каждый столбец должен содержать цифры 1-9 без повторения.
- Каждый из девяти блоков размера 3 х 3 должен содержать цифры 1-9 без повторения.

Доска судоку (частично заполненная) может быть верной, но не обязательно разрешимой. Только заполненные ячейки должны быть проверены в соответствии с указанными правилами. Предложите многопоточное решение.

### Пример 1:
Вход: доска = 
```
[["5","3",".",".","7",".",".",".","."],
["6",".",".","1","9","5",".",".","."],
[".","9","8",".",".",".",".","6","."],
["8",".",".",".","6",".",".",".","3"],
["4",".",".","8",".","3",".",".","1"],
["7",".",".",".","2",".",".",".","6"],
[".","6",".",".",".",".","2","8","."],
[".",".",".","4","1","9",".",".","5"],
[".",".",".",".","8",".",".","7","9"]]
```
Выход: true

### Пример 2:
Вход: доска = 
```
[["8","3",".",".","7",".",".",".","."],
["6",".",".","1","9","5",".",".","."],
[".","9","8",".",".",".",".","6","."],
["8",".",".",".","6",".",".",".","3"],
["4",".",".","8",".","3",".",".","1"],
["7",".",".",".","2",".",".",".","6"],
[".","6",".",".",".",".","2","8","."],
[".",".",".","4","1","9",".",".","5"],
[".",".",".",".","8",".",".","7","9"]]
```
Вывод: false  
Пояснение: То же самое, что и в примере 1, за исключением того, что 5 в левом верхнем углу
изменяется на 8. Поскольку в левом верхнем блоке 3x3 есть два 8, то судоку неверна.

## Задание 5. Вычисление числа Пи (6 баллов)

Напишите программу, которая вычисляет число Пи при помощи ряда Лейбница:
$$
1 - \dfrac{1}{3} + \dfrac{1}{5} - \dfrac{1}{7} + \dfrac{1}{9} - \dfrac{1}{11} + \dots = \dfrac{\pi}{4}
$$
Количество потоков определяется параметром. Расчет числа завершается при вводе пользователя на консоль команды “stop”. Тогда программа должна как можно скорее завершиться, собрать частичные суммы ряда и вывести полученное приближение числа.

Используйте глобальную флаговую переменную. Вычислительные потоки должны просматривать значение флага через некоторое количество итераций, например, через 1000000, и завершаться, если флаг установлен. Подумайте, как минимизировать ошибку, обусловленную тем, что разные потоки к моменту завершения успели пройти разное количество итераций.
