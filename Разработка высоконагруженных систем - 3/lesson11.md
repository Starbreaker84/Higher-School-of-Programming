## Двухфазная блокировка

На протяжении десятков лет в СУБД использовался фактически только один алгоритм сериализуемости:  **двухфазная блокировка** . При конкурентной записи одного объекта двумя транзакциями такая блокировка гарантирует, что вторая записывающая транзакция будет ждать завершения первой (её прерывания или фиксации). Двухфазная блокировка напоминает обычную, но дополняется более сильным требованием: допускается конкурентное чтение одного объекта несколькими транзакциями при условии, что никто его в данный момент не записывает.

Проблема 37. Для выполнения операции записи двухфазная блокировка требует монопольный доступ к объекту: записывающие транзакции блокируют не просто другие записывающие транзакции, но и читающие и наоборот. Но это не соответствует правилу изоляции снимков состояния (читающие транзакции никогда не блокируют записывающие, а записывающие никогда не блокируют читающие).

Решение. Это не столько проблема, сколько ключевое отличие между изоляцией снимков состояния и двухфазной блокировкой. Двухфазная блокировка, обеспечивая сериализуемость, защищает от всех обсуждавшихся выше состояний гонки, включая потери обновлений и асимметрию записи.

## Двухфазная блокировка

Проблема 38. На каждом объекте в базе данных имеется блокировка, которая может находиться или в разделяемом (shared mode), или в монопольном режиме (exclusive mode). Из-за большого количества блокировок часто встречается ситуация, когда транзакция A ждет снятия блокировки транзакции B и наоборот. Она называется **взаимной блокировкой** (deadlock) или  **клинчем** .

Решение. База данных обычно умеет автоматически обнаруживать взаимные блокировки между транзакциями и прерывает одну из них, чтобы остальные могли продолжить работу. Приложению в таком случае придётся повторно выполнять прерванную транзакцию.

### Многооператорные транзакции (задания)

======= 33. В СУБД с однопоточным последовательным выполнением транзакций допускается ...

[ ] код транзакции в виде триггера

[ ] многооператорная транзакция

[ 1] код транзакции в виде хранимой процедуры

======= 34. Если СУБД использует свой диалект SQL ...

[ ] увеличивать количество машин

[ ] активнее применять односекционные транзакции

[ 1] активнее применять транзакции только для чтения

======= 35. Многосекционные транзакции работают намного медленнее потому что ...

[ 1] хранимые процедуры необходимо выполнять во всех секциях строго синхронно

[ ] хранимые процедуры необходимо выполнять во всех секциях строго последовательно

[ ] хранимые процедуры необходимо выполнять во всех секциях строго параллельно
