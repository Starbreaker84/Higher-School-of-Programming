### 14. B-деревья

Журналированные индексы достаточно популярны, но это отнюдь не самый распространенный тип индексов. Наиболее широко используемая индексная структура — это [B-дерево](http://skillsmart.ru/algo/15-121-cm/yj8o1ce8xe.html).

Появившиеся 50 лет назад, B-деревья очень хорошо выдержали испытание временем. Они остаются стандартной реализацией индексов практически во всех реляционных СУБД, да и многие нереляционные базы данных тоже их активно применяют. Аналогично SS-таблицам, B-деревья хранят пары ключ-значение в отсортированном по ключу виде, что позволяет эффективно выполнять поиск значения по ключу и запросы по диапазонам. Но на этом сходство заканчивается: конструктивные принципы B-деревьев совершенно другие.

Журналированные индексы разбивают базу данных на сегменты переменного размера (например, десятки мегабайтов), и всегда записывают их на диск последовательно. В отличие от них,  **B-деревья разбивают БД на блоки или страницы фиксированного размера** , как правило 4 килобайта (иногда больше), и читают/записывают по одной странице за раз. Такая конструкция лучше подходит для низкоуровневого аппаратного обеспечения, поскольку диски в файловой системе тоже разбиваются на блоки фиксированного размера.

Все страницы имеют свой адрес/местоположение, благодаря чему одни страницы могут ссылаться на другие -- аналогично указателям, только на диске. Из этих ссылок и формируется дерево страниц. Одна из страниц назначается корнем B-дерева -- с него начинается любой поиск ключа в индексе. Данная страница содержит несколько ключей и ссылки на дочерние страницы. Каждая из них в свою очередь отвечает за непрерывный диапазон ключей, а ключи, располагающиеся между ссылками, указывают на расположение границ этих диапазонов.

Количество ссылок на дочерние страницы на одной странице B-дерева называется  **коэффициент ветвления** .

Такой алгоритм гарантирует, что дерево останется сбалансированным, то есть глубина B-дерева с n ключами будет равна O(log n). Большинству баз данных достаточно деревьев глубиной три или четыре уровня, поэтому СУБД не приходится ходить по множеству ссылок на страницы с целью найти нужную. Четырёхуровневое дерево страниц по 4 Кбайт с коэффициентом ветвления 500 может хранить до 256 терабайт информации.

**Базовая операция записи B-дерева -- перезапись страницы на диске новыми данными** . Предполагается, что эта перезапись не меняет расположение страницы в дереве, то есть все ссылки на неё остаются неизменными. Это качественное отличие от журналированных индексов, например, LSM-деревьев, в которых происходит только дописывание в файл (и постепенное удаление устаревших файлов), но не изменение существующих файлов.

Перезаписывание страницы на диске можно трактовать уже как реальную операцию, выполняемую средствами аппаратного обеспечения. На магнитном жестком диске это означает перемещение головки диска в нужное место, ожидание поворота вращающейся пластины в нужное положение и перезапись соответствующего сектора новыми данными. С твердотельными накопителями однако ситуация посложнее, поскольку SSD должен стирать и перезаписывать сразу довольно большие блоки на микросхеме памяти.


## B-деревья (задания)

======= 48. Стандартная реализация индексов в сегодняшних СУБД -- это ...

[ ] журнал

[ ] SS-таблица

[1 ] B-дерево

[ ] LSM-дерево

[ ] красно-чёрное дерево

[ ] сбалансированное дерево

======= 49. B-деревья состоят из ...

[ 1] ссылок на страницы

[ ] ссылок на сегменты

[ ] ссылок на узлы

[ ] ссылок на ключи

======= 50. Базовая операция записи в B-дереве -- это ...

[ ] создание новой страницы в дереве

[ 1] перезапись страницы без изменения её положения

[ ] дописывание в файл

[ ] перезапись сектора на жёстком диске
