## 17. Символы и строки

**17.1. Символы**

Символы (тип char) берутся в одинарные кавычки, а хранятся как двухбайтные символы Unicode.

```
let C = '\n'
```

**17.2. Строки**

Строки (тип string) заключаются в двойные кавычки.

```
"\n\r\t"
```

Допускается принятый в .NET синтаксис записи строк, когда для упрощения записи escape-последовательностей (без второго символа \) в начале ставится символ @.

Для доступа к конкретному символу строки предлагается синтаксис индексатора .[ ] с указанием индекса символа, начиная с нуля.

```
let S = "12345"
let C = S.[2] // C = '3'
```

Инфиксный оператор + сцепляет две строки друг с другом.

```
let S = "12" + "345" // S = "12345"
S + "" = S // true
```

Функция string преобразует значение другого типа (например, число или символ) в строку.

```
string '3' // "3"
string 3.14 // "3.14"
string true // "True" 

let circleLen = fun R -> 2.0 * 3.14 * R
string circleLen // "Test+clo@6"
```

Аналогично работает приведение типов и для int, float и других базовых типов, названия которых можно использовать как функции.

```
printfn "%d" (int 3.14) // 3
```

**17.3. Стандартные библиотеки .NET**

По умолчанию в F# доступна стандартная библиотека .NET System и все входящие в неё функции:

```
printfn " = %d" (String.length "123") // длина строки "123" = 3
```

**17.4. Задания**

17.4.1. Напишите функцию pow: string * int -> string, где pow(s,n) выдаёт строку s, повторенную n раз.

17.4.2. Напишите функцию-предикат isIthChar: string * int * char -> bool, где isIthChar(s,n,c) проверяет, равен ли n-й (начиная с нуля) символ строки s символу c.

17.4.3. Напишите функцию occFromIth: string * int * char -> int, где occFromIth(s,n,c) возвращает количество вхождений символа с в строке s, начиная с позиции n.

Шаблон для отправки на сервер:

```
// 17.1
let rec pow  ...

// 17.2
let rec isIthChar  ...

// 17.3
let rec occFromIth  ...
```

**[[ предыдущее ]](https://skillsmart.ru/fp/fsh/i7ae7a4e59.html)**

---

**Ответы на задания 16**

```
let notDivisible(n, m) = m % n = 0 

let rec prime_rec = function 
  | (1, n) -> false 
  | (k, n) -> (n % k = 0) || prime_rec(k - 1, n) 

let rec prime = function 
  | 1 -> false 
  | 2 -> true 
  | n -> not (prime_rec(n / 2, n)) 
```
