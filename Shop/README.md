## Лабораторная работа 2

# Магазин

Есть **Товары** , которые продаются в **Магазинах**.

У **магазинов** есть код (уникальный), название (не обязательно уникальное) и адрес.

У **товаров** есть код (уникальный), название (не обязательно уникальное).

В каждом магазине установлена своя цена на товар и есть в наличии некоторое количество
единиц товара (какого-то товара может и не быть вовсе).

Написать методы для следующих операций:

```
1. Создать магазин;
2. Создать товар;
3. Завезти партию товаров в магазин (набор товар-количество с озможностью установить/изменить цену);
4. Найти магазин, в котором определенный товар самый дешевый;
5. Понять, какие товары можно купить в магазине на некоторую сумму (например, на 100 рублей можно купить три кг огурцов или две шоколадки);
6. Купить партию товаров в магазине (параметры - сколько каких товаров купить, метод возвращает общую стоимость покупки либо её невозможность,
если товара не хватает);
7. Найти, в каком магазине партия товаров (набор товар-количество)
имеет наименьшую сумму (в целом). Например, «в каком магазине дешевле всего
купить 10 гвоздей и 20 шурупов». Наличие товара в магазинах учитывается!
```
Для демонстрации необходимо создать минимум 3 различиных магазина, 10 типов товаров
и наполнить ими магазины.