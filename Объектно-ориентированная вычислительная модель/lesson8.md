## 4. Программирование с использованием наследования

### Корректное использование наследования

**Понимание наследования через типы** .
В данном случае мы считаем, что классы -- это типы, а классы-потомки -- это подтипы. Представление классов как типов соответствует принципу, что классы должны моделировать сущности реального мира, или некоторые абстрактные версии этих сущностей. Тогда  **классы должны удовлетворять свойству подстановки (LSP)** : каждая операция, которая работает для объекта класса C, также работает аналогичным образом для объектов подкласса С (классы-наследники не должны ломать "интерфейсы" родительских классов). Большинство объектно-ориентированных языков (Java, C++, C#...) разработаны именно под данный случай, но в них через свободное переопределение методов можно довольно легко нарушить этот принцип.

**Понимание наследования через структуру** .
С этой точки зрения, наследование -- это просто ещё один инструмент программирования, который используется для структурирования программы. Эта точка зрения категорически не рекомендуется, поскольку такая работа с классами вообще не учитывает свойство подстановки. Структурный подход -- это бесконечный источник ошибок и плохих проектов.

С точки зрения типов, каждый класс стоит на своих собственных ногах, так сказать -- как полноценный АТД. Это справедливо даже для классов, имеющих подклассы; с точки зрения класса-наследника, родительский класс -- это АТД с единственным доступом к нему через методы (атрибуты родителя скрыты от прямого доступа). А с точки зрения структуры классы становятся просто строительными лесами, которые используются только для структурирования программы.

В подавляющем большинстве случаев наследование должно полноценно поддерживать вариант "классы как типы"; иная схема приводит к тонким и губительным ошибкам. Пожалуй, единственный случай, когда допускается структурный подход -- если требуется менять поведение самой объектной системы как таковой. Но всё равно этим должны пользоваться только опытные программисты, и только если не сработали другие подходы (например, мета-объектные протоколы).

### Конструирование иерархии через следование типу

На курсе по декларативной парадигме отмечалось, что когда мы пишем рекурсивную программу, полезно сперва определить тип обрабатываемой структуры данных, и затем конструировать программу через следование этому типу. Аналогичный принцип хорошо подходит и при конструировании иерархий наследования.

Возьмём, для примера, список List<T>, который, напомню, рекурсивно определён как либо nil (отсутствие значения), либо элемент-голова типа T, сцепляемый с оставшимся списком List<T>. В парадигме ООП определяем абстрактный родительский класс ListClass (например, с методами isNil и append), у которого будут два наследника: NilClass и ConsClass. Экземпляром класса NilClass будет пустой список (что сохраняет принцип подстановки), и тогда ConsClass естественно выражается через NilClass и ConsClass.

### Общие/обобщённые классы (классы-генерики)

Peter Van Roy под "generic class" понимает не программистские генерики, когда класс параметризуется другим типом/классом (например, List<T>), а класс, который определяет часть функциональности АТД. Сам по себе он должен быть полностью реализован, прежде чем его можно будет использовать для создания объектов.

Предлагаются два способа определения общих классов: через наследование ООП, и через программирование высшего порядка, и на самом деле первый способ -- лишь синтаксический вариант второго. Другими словами,  **наследование -- это стиль программирования, базирующийся на программировании высшего порядка** .

Универсальный способ делать классы "более общими" в ООП -- это использовать абстрактные классы. Например, имеется абстрактный класс GenericSort, который реализует алгоритм сортировки списка, по определению подразумевающий сравнение сортируемых элементов. Однако такое сравнение зависит от типов этих элементов, поэтому от GenericSort наследуются классы (например, IntegerSort, StringSort), которые реализуют такое сравнение уникальным для соответствующего типа образом.

Второй путь создавать классы-генерики сам по себе более общий, но во многих массовых языках поддержка такого способа отсутствует. Так как сами классы в идеале -- значения первого класса, м ы создаём функцию, которая получает подходящую функцию сравнения (например, функцию сравнения целых или строк) и возвращает класс, "настроенный" на это сравнение. Но такое динамическое создание классов, как говорилось, возможно далеко не везде.

Поэтому, по сути, мы используем наследование, чтобы "подключить" одну операцию к другой. Наследование -- это просто форма программирования высшего порядка, когда первая операция передаётся второй. В чём разница между этими двумя техниками? В большинстве языков программирования иерархия наследования должна быть явно определена на этапе компиляции. Это даёт определённые преимущества: компилятор может генерировать лучший код, а тайп-чекер сможет делать больше проверок на ошибки. Программирование высшего порядка позволяет определять новые классы непосредственно во время выполнения программы. Это дает большую гибкость, а традиционная расплата -- усложнение отладки.

### Множественное наследование

Множественное наследование во многих популярных языках не поддерживается, а там, где поддерживается, пользуются им очень ограниченно. Множественное наследование позволяет потенциально представлять сложные вещи довольно удобными конструкциями. Например, Бертран Мейер приводит пример, когда имеется класс "геометрическая фигура", от которого наследуются обычные фигуры. Однако фигура может также составляться из нескольких фигур, и тогда соответствующий класс "составная фигура" может наследоваться как от "геометрической фигуры", так и от контейнера -- например, от списка.

В реальных проектах подобная поддержка множественного наследования бывает важна -- когда требуется в дополнение к некоторому "основному" наследованию добавить совершенно иные характеристики (например, свойство упорядоченности, сортируемости, "рисуемости", композиционности...). По этой причине в языках C# и Java например вместо множественного наследования классов поддерживается множественное наследование интерфейсов.


### Программирование с использованием наследования

======= 24. Классы должны удовлетворять свойству подстановки ...

[ ] если считаем, что классы нужны для структурирования программы

[1] если считаем, что классы -- это типы

======= 25. Наследование -- это ...

[1] стиль программирования, базирующийся на программировании высшего порядка

[ ] один из трёх китов ООП

[ ] использование абстрактных классов

======= 26. Множественное наследование (классов или интерфейсов) полезно для ...

[1] дополнения класса совершенно иными характеристиками

[ ] дополнения класса схожими характеристиками
