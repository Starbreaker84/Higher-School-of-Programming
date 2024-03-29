## Проблемы изоляции и сериализуемость

Проблема 31. В реплицируемых базах данных ведутся копии сразу в нескольких узлах, и данные могут потенциально изменяться одновременно в различных узлах. Но блокировки и операции сравнения с обменом предполагают существование только одной актуальной копии данных, однако в случае репликации без ведущего узла или с несколькими ведущими узлами допускается несколько одновременных операций записи и их асинхронная репликация.

Решение. В реплицируемых СУБД разрешается создавать несколько конфликтующих версий значения (так называемые  **родственные значения** ), а их разрешение и слияние постфактум выполняет либо само приложение, либо специализированные алгоритмы базы. В этом контексте хорошо работают атомарные операции, особенно если они коммутативны, то есть результат при их использовании в разном порядке в различных репликах остаётся тем же самым. Например, приращение счетчика -- коммутативная операция.

## Проблемы изоляции и сериализуемость

Проблема 32. Существуют менее заметные конфликты, которые не описываются "грязными" операциями записи или потерей обновления -- в ситуациях, когда две транзакции обновляют два разных объекта. Например, в одном объекте хранится количество единиц некоторого разделяемого ресурса, на которые ссылаются другие объекты и которые они используют. Если в этом объекте осталась только одна единица ресурса, и к нему конкурентно обратились две транзакции (каждая запрашивает по единице для своего модифицируемого объекта), то в итоге в нём останется не ноль, а минус одна единица ресурса, и консистентность системы будет нарушена. Такая аномалия носит название **асимметрии записи** (write skew) и возникает только при параллельной работе транзакций: если бы они выполнялись последовательно, то вторая транзакция, обнаружив, что требуемый ресурс исчерпан, выполнила бы другие действия.

В общем случае, подобная асимметрия происходит, когда две или более транзакций читают одни и те же объекты с последующим обновлением некоторых из них (различные транзакции могут обновлять разные объекты). В частности, когда разные транзакции обновляют один объект, происходит "грязная" операция записи или потеря обновления (в зависимости от хронометража).

Атомарные однообъектные операции тут не помогут, поскольку в транзакции участвует несколько объектов. К сожалению, не помогает и автоматическое обнаружение потерянных обновлений: ни при воспроизводимом чтении в PostgreSQL или MySQL, ни при сериализуемых транзакциях Oracle, ни на уровне изоляции снимков состояния SQL Server.

Решение. Универсальный подход -- организация поддержки **дополнительных логических требований/ограничений к системе** в ситуациях, когда одна транзакция меняет некоторое значение, которое необходимо для выполнений другой транзакции.

Некоторые СУБД позволяют задавать ограничения целостности, за соблюдением которых затем следят они сами (например, ограничения уникальности значений, ограничения внешних ключей или ограничения, накладываемые на конкретное значение). Однако в практических конфликтных ситуациях оказывается, что такие ограничения необходимо накладывать на несколько объектов, что весьма затруднительно для полноценной реализации. Такие ограничения более-менее удовлетворительно реализуются с помощью триггеров или хранимых процедур. Ещё одно неплохое решение -- блокировка объектов и на запись, и на чтение, но она вполне может создать проблемы в плане производительности (большие очереди ожидания).

## Проблемы изоляции и сериализуемость

Проблема 33. Пусть создана условная многопользовательская игра в шахматы реального времени (когда игроки могут делать ходы одновременно). Мы можем корректно задействовать блокировку для предотвращения потери обновлений -- гарантировать, что два игрока не смогут передвинуть одну и ту же фигуру одновременно. Однако такая блокировка не препятствует игрокам перемещать две различные фигуры на одну и ту же клетку на доске, или совершать другой нарушающий правила игры ход -- возникает асимметрия записи.

Другие типичные проблемные ситуации:

-- два пользователя на онлайн-ресурсе выбирают себе имена, уникальные в системе, однако если они выберут одинаковые "свободные" имена, возникнет конфликт;

-- два сервиса списания денег (например, ежемесячные платежи за интернет и телефон) могут одновременно обратиться к счету конкретного пользователя, где денег достаточно лишь на один платёж.

В общем случае, некоторая операция записи в одной транзакции вполне может изменить потенциальный результат запроса на поиск в другой транзакции. Такие случаи называются  **фантомы (phantom)** . Когда транзакция выполняет и чтение и запись данных, могут возникать особенно запутанные случаи асимметрии записи, поскольку отсутствует прямой объект, на который можно установить блокировку (например, он ещё не создан, или доступен лишь косвенно).

## Проблемы изоляции и сериализуемость

Уровни изоляции реализованы в разных СУБД сильно по-разному, и на уровне кода самого приложения трудно понять, будет ли он безопасно выполняться на нужном уровне изоляции, и уж тем более фактически не существует удобных инструментов для обнаружения состояний гонки. Тестирование тоже практически не способно обнаружить проблемные ситуации конкурентного доступа, поскольку они недетерминированы и возникают в случае неудачного хронометража. Эта проблема известна ещё с конца 1970-х, когда появились слабые уровни изоляции.

Решение. Исследователи всегда дают один и тот же основной совет: применяйте сериализуемость.

**Сериализуемость** (serializability) считается самым сильным уровнем изоляции и гарантирует, что даже при конкурентном выполнении транзакций результат останется таким же, как и в случае их последовательного выполнения. Фактически, СУБД предотвращает все возможные состояния гонки.

Как уже отмечалось, простейший способ избежать проблем с конкурентным доступом -- вообще отказаться от него. Выполняем только по одной транзакции последовательно, в одном потоке. Подобная изоляция транзакций по определению сериализуема. Интересно, что лишь 15 лет назад эта очевидная идея нашла поддержку у производителей СУБД -- во многом потому, что оперативная память стала достаточно дешёвой, и во многих сценариях удаётся хранить в оперативной памяти активный набор данных полностью. В результате транзакции выполняются гораздо быстрее, поскольку не нужно ждать загрузки данных с жёсткого диска. А так как длительные аналитические запросы обычно только читают информацию, их можно выполнять на согласованном снимке состояния вне цикла последовательного выполнения.

### Атомарные операции (задания)

======= 25. Проблема потерянного обновления (изменение счётчика двумя транзакциями одновременно...) решается с помощью...

[ ] атомарных операций чтения

[ 1] атомарных операций записи

[ ] блокировки объекта на чтение

[ ] блокировки объекта на запись

======= 26. Если функциональности встроенных атомарных операций недостаточно, можно выполнять явную блокировку объектов ...

[ ] на уровне СУБД

[ 1] на уровне приложения

======= 27. Потери обновлений можно обнаруживать автоматически, если обновление объекта допускается ...

[ 1] если его значение не менялось с момента его прошлого чтения

[ ] если его значение не менялось с момента его последней модификации
