## Репликация

Проблема 6. Если реплицируемые данные не меняются со временем, то нужно просто однократно скопировать их на каждый узел. Но что делать с изменениями реплицированных данных?

Решение. На практике используют три популярных алгоритма репликации изменений между узлами:  **репликация с одним ведущим узлом (single-leader), с несколькими ведущими узлами (multi-leader), и без ведущего узла (leaderless)** .

## Репликация

Проблема 7. Как обеспечить присутствие всех данных во всех репликах?
Каждая операция записи в базу должна учитываться каждой репликой, иначе нельзя гарантировать, что реплики содержат одни и те же данные.

Решение. Репликация с ведущим узлом (leader-based replication).

1. Одна из реплик назначается  **ведущим (leader) узлом** . Клиенты отправляют свои запросы ведущему узлу, который записывает новые данные сначала в свое локальное хранилище.
2. Другие реплики называются  **ведомыми (followers) узлами** . Когда ведущий узел записывает в своё хранилище новые данные, он также отправляет информацию об изменениях всем ведомым узлам -- через **журнал репликации (replication log)** или  **поток изменений (change stream)** .
3. При чтении данных из базы клиент может выполнить запрос к ведущему узлу, или к любому из ведомых -- они также называются  **реплики чтения (read replicas), подчинённые узлы (slaves), вспомогательные узлы (secondaries), горячий резерв (hot standbys)** . Однако запросы на запись разрешено отправлять только ведущему.

Эта схема применяется в PostgreSQL, MySQL, Oracle Data Guard, MongoDB, распределённых брокерах сообщений Kafka, очередях сообщений RabbitMQ и др.

### Репликация с ведущим узлом (задания)

======= 7. Клиенты отправляют свои запросы на запись ...

[ ] к подчинённым узлам

[ ] к любому из ведомых узлов

[1 ] к ведущему узлу

======= 8. Ведомые узлы получают информацию об изменениях ...

[ ] из журнала репликации

[ ] от любого из ведомых узлов

[ 1] от ведущего узла

======= 9. Клиент выполняет запрос на чтение ...

[ 1] к любым узлам

[ ] только к ведомым узлам

[ ] только к ведущему узлу
