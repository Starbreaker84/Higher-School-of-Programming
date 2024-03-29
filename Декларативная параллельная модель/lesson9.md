### Трансдьюсеры и конвейеры

Добавим новый потоковый объект между поставщиком и потребителем. Этот объект читает поток поставщика, и создает другой поток, который считывается потребителями. Такой промежуточный объект называется  **трансдьюсер** .

В общем случае, последовательность потоковых объектов, каждый из которых формирует данные для следующего, называется  **конвейер** . Поставщик в таких схемах иногда называют  **источник (source)** , а потребитель --  **сток (sink)** .

Одна из наиболее популярных задач, где применяются трансдьюсеры -- это фильтр, когда надо выдавать далее не все входные элементы, а только соответствующие определённому условию (предикату), для чего удобно применять программирование высшего порядка. Например, пропускать надо только чётные значения, или непустые строки длиной не более 1024 символа, и т. п. Конвейеры фильтров на практике нередко создаются динамически, что существенно повышает гибкость программы.

Как уже говорилось, формально реализация потока в виде списка в декларативной модели полностью иммутабельна в том смысле, что помещённые поставщиком в поток элементы остаются в нём навсегда. На практике, конечно, это неэффективно, и "устаревшие" значения из списка-потока должны удаляться по некоторому алгоритму (условным сборщиком мусора). Формальная модель потока это и подразумевает: когда значение больше не "нужно", оно пропадает, а нужность его определяется только тем, было ли оно считано. Если потребитель один, то значение в потоке пропадает сразу после его "получения" потребителем (потоковый элемент -- это фактически аналог сетевого пакета). Если потребителей несколько, то ситуация становится менее определённой: значение можно удалять, когда оно было прочитано всеми потребителями, однако в общем случае число потребителей может меняться динамически.

### Ленивое и жадное выполнение

Но что произойдет, если поставщик будет формировать поток (записывать элементы в список) гораздо быстрее, нежели потребитель может их потреблять? Если так будет продолжаться достаточно долго, то размер потока может быстро вырасти и монополизировать все системные ресурсы.

В таких случаях применяется различное ограничение скорости, с которой поставщик генерирует новые элементы. Это ограничение требует, чтобы выполнялось некоторое глобальное условие (например, использование памяти до некоторого порога). Когда такое условие нарушено, поставщик временно прекращает формирование потока, ожидая, когда потребители считают из него значения. Реализация такой схемы однако подразумевает, что между поставщиком и потребителем существует некоторая обратная связь по управлению потоком.

Классический вариант подобной реализации --  **параллелизм, управляемый по запросу** , более известный как  **ленивое выполнение** . Поставщик генерирует очередной элемент, только когда какой-нибудь потребитель запросит его явно (генерация элементов без учёта "мнения" потребителя называется  **жадное выполнение** ). Простейший способ организации такого запроса -- это использование dataflow-переменных. Добавление в хвост списка-потока несвязанной переменной теперь выполняет не поставщик, а сам потребитель. Поставщик же наоборот следит за появлением такой переменной, и как только обнаруживает её, связывает с очередным готовым значением.

### Ленивое и жадное выполнение

Однако обе эти схемы излишне категоричны. В жадном выполнении поставщик полностью свободен, что может привести к захвату ресурсов, а в ленивом, наоборот, полностью ограничен -- но тогда возникает обратная ситуация, связанная со снижением пропускной способности. Если потребитель запрашивает сообщение, то поставщик должен его вычислить, а тем временем потребитель (скорее всего, не один) ждёт. Если бы поставщику было позволено хотя бы немного опережать потребителя, то тому не пришлось бы ждать.

Хорошее прикладное решение (комбинация жадного и ленивого выполнения) --  **ограниченный буфер** . Это трансдьюсер, который хранит некоторое количество элементов (не более заданного максимального количества n). Поставщику разрешается опережать потребителя, но только до тех пор, пока буфер не будет заполнен. Потребитель конечно может брать элементы из буфера мгновенно, без ожидания. А когда в буфере меньше n элементов, поставщику разрешается выдавать в него новые элементы, пока буфер не заполнится. Такой подход поддерживает пропускную способность на высоком уровне.

Однако реализация такой схемы даже в чистой декларативной модели с dataflow-переменными неинтуитивна, потому что требуется дополнительное управление поставщиком (когда и какую порцию элементов ему надо выдать в буфер). Тут подразумевается ленивое выполнение (поставщик ждёт, когда трансдьюсер выдаст новую несвязанную переменную), однако практика показывает, что скрытое ленивое выполнение через некоторое расширение декларативной модели оказывается гораздо выразительнее в плане ясности кода, нежели явное. Продуктивную реализацию данной схемы лучше выполнять в stateful-модели, когда мы определяем АТД с двумя операциями get и put, а внутри можем реализовать это всё очень компактно и эффективно.

**Жадное выполнение -- это то, что происходит, когда буфер имеет бесконечный размер.
Ленивое выполнение -- это то, что происходит, когда буфер имеет нулевой размер.**

### Ленивое и жадное выполнение

Есть ещё один, уже менее эффективный, способ управления -- это изменение относительных приоритетов нитей поставщика и потребителя. Делаем это так, что потребители считывают элементы быстрее, нежели поставщики могут их "производить". Недостаток этого способа в том, что он хрупок и зависит от сложности производства самого элемента и его последующего считывания.

**Изменение приоритетов нитей никогда не должно использоваться для организации "правильной" работы** . Система должна работать правильно независимо от того, каковы приоритеты нитей. Изменение приоритетов -- это просто оптимизация производительности, повышение пропускной способности и т. п.

23 2

24 1

25 2


======= 23. Кооперативный параллелизм -- это ...

[ ] каждый процесс заинтересован только в собственной производительности

[ ] нити работают вместе над глобальной задачей

======= 24. Конкурентный параллелизм -- это ...

[ ] каждый процесс заинтересован только в собственной производительности

[ ] нити работают вместе над глобальной задачей

======= 25. Поток -- это ... (выберите неверное утверждение)

[ ] потенциально неограниченный список сообщений

[ ] трансдьюсер

[ ] средство организации связи между нитями
