## 3. Масштабируемость

Даже если на сегодняшний момент система работает надежно, нет никаких гарантий, что она будет так же работать и в будущем. Завтра нагрузка может десятикратно возрасти, или начнут поступать значительно большие объемы данных для обработки.

**Масштабируемость (scalability)** — способность системы справляться с возросшей нагрузкой. Измерить её каким-то единственным критерием невозможно. Масштабируемость подразумевает, что у вас есть чёткие варианты решения потенциальных проблем, когда система сильно вырастет.

Во-первых,  **надо количественно описать текущую нагрузку на систему** , и понять, что случится с системой, и что надо делать, если эта нагрузка возрастёт в 2 раза, в 10 раз, в 100 раз.

Набор числовых характеристик нагрузки называют  **параметрами нагрузки** . Их оптимальный выбор зависит от мастерства архитектора системы.

Это может быть количество запросов к серверу в секунду, отношение количества операций чтения к количеству операций записи в базе данных, количество одновременно активных пользователей в некоторой игровой зоне, частота успешных обращений в кэш и т. п. В одних случаях будет важно среднее значение, в других -- набор предельных значений.

Во-вторых, определившись с параметрами нагрузки,  **надо понять, что произойдёт при их росте** . Для этого сначала определяем, что случится с производительностью системы, если её ресурсы останутся неизменными на текущем уровне, и затем разбираемся, насколько нужно увеличить эти ресурсы, чтобы производительность сохранилась на текущем уровне.

Однако скалярные показатели нагрузки плохо описывают динамику работы системы. Например, время отклика сервера может существенно разниться в разное время суток, в разные дни недели, и т. д. Поэтому  **характеристики нагрузки надо рассматривать как (нормальное) распределение значений** .

Для этого надо прежде всего рассчитать медиану (среднее значение) за подходящий период. Например, медианное время отклика сервера 100 мс означает, что половина пользователей получает ответ от сервера быстрее 100 мс, а другая половина -- медленнее.

На практике, пользователи, для которых время отклика сервера медленное, часто оказываются самыми ценными, потому что, например, они сделали большое количество покупок, и серверу требуется больше времени, чтобы обрабатывать их запросы. Поэтому оптимизировать работу сервера надо с прицелом именно на них. Для более точного отслеживания данного нюанса используют **процентили** -- сравнительную характеристику некоторого значения критерия по отношению к его "коллегам".

Например, процентиль 90% для пользователя, отклик сервера для которого составляет 200 мс, означает, что у 90% пользователей отклик сервера быстрее 200 мс. И, соответственно, если этот клиент важен, то надо разбираться, почему подавляющему большинству пользователей (скорее всего, менее важных) сервер отвечает быстрее.

## Общие принципы масштабирования:

как справиться с нагрузкой

Одна из наиболее типичных, классических проблем масштабирования -- так называемая  **блокировка головы очереди** .

Сервер физически способен параллельно обрабатывать относительно небольшое количество заданий (ограниченное, например, количеством ядер процессора или объёмом оперативной памяти), поэтому даже небольшое количество медленных запросов приводит к задержке последующих запросов, даже если они обрабатываются сервером быстро. Система всё равно будет характеризоваться низким временем отклика из-за долгого ожидания завершения предыдущего запроса.

Иногда это относительно успешно решается в асинхронной модели обработки запросов, когда потенциально быстрые запросы удаётся "вытаскивать" и обрабатывать в первую очередь, однако всё равно общий потенциал системы ограничивается её физическими аппаратными ресурсами.

## Универсальные подходы снижения нагрузки

Существует пять наиболее универсальных подходов к снижению нагрузки.

1) **Базы данных** (БД, в общем случае автономные системы управления базами данных СУБД): храним накапливающиеся данные в промежуточных хранилищах, чтобы те или иные приложения, когда придёт время, могли в дальнейшем найти их и продолжить обработку;
2) **Поисковые индексы** : механизм ускорения поиска в условном хранилище информации (базе данных, файловой системе, ...) по ключевым словам, быстрой фильтрации данных различными способами;
3) **Кэши** : запоминаем результат частой ресурсоемкой операции и выдаём его готовым вместо реальных вычислений;
4) **Пакетная обработка** : периодически выполняем разовую длительную (как правило, в фоне) обработку больших объёмов данных (например, рассчитываем квартальный отчёт);
5) **Потоковая обработка** : организуем обмен сообщениями между процессами для их одновременной асинхронной работы, как правило, над общим набором/базой данных.

На практике каждый из этих подходов представляется самыми разными технологиями и реализуется самыми разными способами. Кроме того, при попытке плотно комбинировать эти подходы техническая сложность системы быстро растёт. Поэтому важно чётко определиться, какие конкретно инструменты и методы лучше всего подходят к вашему проекту, и насколько они согласуются друг с другом.

## Два вида масштабирования

Обычно масштабирование разделяют на два вида:
-- **вертикальное масштабирование** — переход на более мощную машину, и
-- **горизонтальное масштабирование** — распределение нагрузки по нескольким более слабым машинам, связанным друг с другом.

На практике сервис, которому достаточно одной рядовой машины, довольно прост в разработке и эксплуатации, даже если его работу приходится синхронизировать с другими сервисами. А мощные машины часто сильно дороги и по цене, и по сопровождению. Поэтому  **практически всегда в highload-системах применяется некая форма горизонтального масштабирования** . Причём стратегически выгоднее оказывается выполнять такое масштабирование на небольшое число достаточно мощных машин, нежели на множество маленьких (например, виртуальных) машин, для оркестровки (синхронного управления) которых приходится дополнительно внедрять тяжёлые и дорогие в эксплуатации системы наподобие Kubernetes. Например, довольно распространённый подход: масштабировать базу данных вертикально (держать на одном узле) до тех пор, пока стоимость дальнейшего масштабирования или иных требований по хорошему отклику не заставят сделать её распределённой.

Существует отдельное инженерное направление проектирования систем, умеющих "самостоятельно" адаптироваться к плохо предсказуемым изменениям нагрузки, однако всё же  **лучше ориентироваться на создание систем, масштабируемых преимущественно "вручную"** : они значительно проще в разработке и более надёжны и предсказуемы в эксплуатации.

Поэтому в программной инженерии наработано много типовых шаблонов, хорошо проверенных практикой -- универсальных блоков для создания масштабируемой системы, ряд которых мы изучаем на данном курсе.

## Масштабирование (задания)

======= 7. Имеется сервис, где для каждого пользователя хранится процент правильно решённых тестов.
Пользователь с процентилем 95% для процента 85% правильных ответов означает что

[ ] у 15% процент правильных ответов больше 95%

[ 1] у 5% процент правильных ответов больше 85%

[] у 5% процент правильных ответов меньше 85%

[ ] у 15% процент правильных ответов меньше 95%

======= 8. К чему приведёт блокировка хвоста очереди?

[] к ухудшению среднего времени обработки запросов

[1] к росту количества отказов в обслуживании

[ ] всё останется без изменений

======= 9. Можно ли сегодня найти готовую СУБД под задачу интенсивной обработки данных, в которой все пять подходов масштабирования уже объединены (БД + индексы + кэши + пакетная обработка + потоковая обработка)?

[1] да, немало СУБД где всё это уже поддерживается под ключ

[] нет, под ключ такое мало где, и интегрировано далеко не всё

======= 10. Когда возникнет потребность в масштабирировании, какой подход преимущественно предпочтителен?
[ ] преимущественно вертикальный

[ 1] преимущественно горизонтальный
