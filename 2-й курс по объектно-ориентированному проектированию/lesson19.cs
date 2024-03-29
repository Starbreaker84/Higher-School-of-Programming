// 1) Наследование подтипов (subtype inheritance)
// Пример: Родительский абстрактный класс Money, и классы наследники Dollar, Euro, Ruble ...

// 2) Наследование с ограничением (restriction inheritance)
// Пример: Родительский класс Master и запечатанный (sealed) класс наследник Slave, с урезанными правами и методами, которые нельзя переопределить
// Пример2: Родительский класс RECTANGLE и класс наследник SQUARE, который получает дополнительное ограничение side1 = side2

// 3) Наследование с расширением (extension inheritance)
// Пример: графический класс POINT и класс наследник MOVING_POINT, имеющий дополнительный атрибут speed, описывающий величину и направление движения точки
// Пример2: класс group, имеющий операцию +, и класс наследник ring, который кроме операции + приобретает ещё и операцию * 
