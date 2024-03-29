### Команда try

**Команда try контролирует некоторый подведомственный ей контекст, реагируя на возникающие в нём ошибки и предоставляя возможность задания реакций на них** (обработки исключительных ситуаций). Программисту предоставляется возможность добавлять собственные виды исключений.

Как правило, данная команда состоит из инструкции try (начало контролируемого контекста), catch (блок, где обрабатываются различные виды возникших исключений), и else (блок, который выполняется, если исключения не возникло). Иногда она дополняется инструкцией finally со своим блоком, который выполняется в заключение всегда, независимо от того, возникла ошибка или нет. Это нужно, например, для выполнения некоторых "очистных" операций наподобие закрытия файла.
Названия этих инструкций и форма их работы и взаимосвязи между ними в разных языках программирования, конечно, могут различаться, однако общая схема -- набор минимальных требований к контролю исключительных ситуаций -- приведена выше.

Допускается опускать любые инструкции кроме начальной try.
