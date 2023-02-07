// Какие из четырёх вариантов скрытия методов доступны?
//
// Python
//
// 1. метод публичен в родительском классе А и публичен в его потомке B.
// Поддерживается в Python полноценно.
//
// 2. метод публичен в родительском классе А и скрыт в его потомке B.
// 3. метод скрыт в родительском классе А и публичен в его потомке B.
// Можно реализовать на "организационном уровне" с помощью исключений в методах, которые скрываются, что может контролироваться тестами.
//
// 4. метод скрыт в родительском классе А и скрыт в его потомке B.
// В Python есть псевдоприватные методы, которые выделяются двойным подчёркиванием перед названием метода.
// Если мы принимаем, что рекомендация приватности (одно подчёркивание) равна приватности, то в Python можно поддержать вариант 4, при котором приватный метод остается приватным в наследнике.
//
// class PrivateMethodsTest():
//     def __almost_private(self):
//         print('Don\'t touch me')
//
//     def _recomended_private(self):
//         print('Don\'t touch me, please')
// Java
//
// class A extends Any {
//     private int a;
//     private int b;
//     private int c;
//     private int d;
//
//     //метод публичен в родительском классе А 
//     //и публичен в его потомке B;
//     public int getA() {
//         return this.a;
//     }
//
//     //метод скрыт в родительском классе А 
//     //и публичен в его потомке B;
//     protected int getB() {
//         return this.b;
//     }
//
//     //метод публичен в родительском классе А 
//     //и скрыт в его потомке B
//     // -- такая видимость методов в иерархии невозможна,
//     // так как в классах-наследниках доступ у метода 
//     //должны быть таким же или более слабым
//     public int getC() {
//         return this.c;
//     }
//
//     //метод скрыт в родительском классе А 
//     //и скрыт в его потомке B.
//     protected int getD() {
//         return this.d;
//     }
// }
// class B extends A {
//
//     @Override
//     public int getA() {
//         return super.getA() + 1;
//     }
//
//     @Override
//     public int getB() {
//         return this.getB() + 2;
//     }
//
//     /*@Override
//     private int getC() {
//         return super.getC();
//     }*/
//
//     @Override
//     protected int getD() {
//         return super.getD() + 4;
//     }
// }