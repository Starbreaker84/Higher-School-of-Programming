### 29. Эффективность по времени и памяти

Декларативное программирование -- это прежде всего программирование. Несмотря на то, что оно обладает сильными математическими свойствами, результатом его получаются всё те же реальные программы, которые работают на реальных компьютерах. Поэтому и тут важно думать о **вычислительной эффективности** (время и память). Эта тема затрагивалась на курсах по алгоритмам и структурам данных, где мы ориентировались на О-большое (наихудший случай).

На практике, из-за того, что программы работают на всё усложняющемся железе и используют весьма сложные возможности стандартных библиотек языка и операционной системы, виртуальные модели памяти и т. п., определить реальную производительность и требуемые ресурсы алгоритма без его привязки к контексту исполнения в общем случае становится почти невозможно. Поэтому применяется также **амортизационный анализ** (когда мы учитываем, что шагами алгоритма могут быть как "лёгкие" операции, так и весьма нагрузочные).

Декларативная модель в алгоритмическом плане -- одна из самых простых, поэтому оценивать эффективность декларативных алгоритмов достаточно легко. В частности, надо активно учитывать распространённость рекурсии, которая, как было показано, довольно часто на практике, при слабой реализации, требует существенных ресурсов по памяти (стек). Но прикладная (не математическая) оптимизация по памяти обычно выполняется значительно проще, нежели оптимизация по производительности.

Во-первых, в декларативной модели сам алгоритм, как мы видели, легче и по известным схемам переписывается в более экономный вид;

во-вторых, под очень многие задачи можно подобрать простую, экономную и хорошо изученную структуру данных (прежде всего из рассмотренных выше);

в-третьих, саму память можно оптимизировать, например, с помощью сборщиков мусора и алгоритмов сжатия, и т. д.

Однако оптимизированный код никогда не бывает "оптимальным" ни в каком математическом смысле. Обычно программа может быть легко улучшена, но лишь до некоторой точки, после чего отдача от её улучшений начинает уменьшаться, а сама программа быстро усложняется, и последующие улучшения дают ещё меньший эффект. Поэтому  **оптимизация не должна выполняться, если в ней нет явной необходимости** .

Оптимизация имеет как хорошую, так и плохую сторону. Хорошая сторона в том, что в целом  **время выполнения большинства приложений в значительной степени определяется работой очень малой части кода программы** . Поэтому оптимизация производительности при реальной необходимости почти всегда может быть ограничена переписыванием только этой маленькой части (иногда буквально нескольких строк).

Плохая сторона в том, что обычно  **не очевидно даже опытным программистам, где эта часть априори находится** . Поэтому эта часть сперва должна быть идентифицирована в процессе работы программы -- как правило, с помощью технологии профилирования.

Оптимизировать (сократить) использование программой оперативной памяти обычно заметно проще, нежели сократить время её выполнения.  **Если память -- это критическая проблема, то хорошим приёмом будет использование алгоритмов сжатия данных** , которые не участвуют в основных вычислениях. Тут придётся искать баланс между экономией пространства и повышением быстродействия.


### Эффективность по времени и памяти (задания)

======= 66. Программирование в декларативной модели...

[x]легче и проще, нежели во всех других парадигмах

[ ]сложнее ряда других парадигм из-за активного применения рекурсии

======= 67. Оптимизация ...

[ ] должна выполняться как можно раньше

[x] не должна выполняться без явной необходимости

======= 68. Для оптимизации ...

[ ] надо глобально переписывать весь код

[ ] надо это учитывать как можно раньше

[x] обычно достаточно переписать небольшую часть кода
