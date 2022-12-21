# ASP.NET-Core-Project-Eatery-POS-System

Eatery POS System
Изпитен проект на Исмаил Сираков за курс по ASP.NET Core към СофтУни.
 
С приложението се организира движението на материали и продукти в обекти за хранене, като се започне от въвеждане на документите за закупените материали, техните трансфери между складовете и се стигне до изразходването им при маркиране на продажба на продукти. 

В приложението е осигурена възможност за въвеждане на видове документи, видове плащания, данни за доставчици, персонал и длъжностите им, различни мерни единици, материали, складове, обекти за продажба, продукти, видове продукти и рецепти за продукти. С цел улесняването на демонстрацията на приложението е предвидено зареждане на примерни данни като имена на фирми с адресите име, длъжности на персонал, закупени материали с количества, обекти, продукти и рецепти към тях.

Администратор на приложението:
Потребител "Администратор": admin@eaterypos.com
Парола: admin12

След стартиране на приложението влезте като "Администратор" с дадените потребителско име и парола.

От меню "Админ" най-напред импортнете примерните основни данни (типове документи, мерни единици, типове плащания, длъжности, типове продукти, обекти и складове). След това импортирайте данните за доставчици и материали ( доставчици с адресите им и материали) и накрая продуктите и рецептите.
От това меню също така може да се добави нова роля, да се добавя роля към даден потребител и да се изтрие роля на потребител.

                    Описание на менютата на приложението.

  Падащо меню "Начални данни".

От това меню могат да се добавят нови "Тип документ", "Мерна единица", "Тип плащане", "Длъжност", "Тип продукт", "Обект" и "Склад".

  Падащо меню "Склад".

От това меню се звеждат закупените материали в даден склад  от "Добави материал в склад", трансферират се материали между складове от "Трансфер на материали", добаят се нови материали, доставчици, градове и адреси от съответните менюта.

  Падащо меню "Маркиране".
  
С помоща на това меню се маркират продадените продукти. Като най-напред се избира обекта от който се извършва продажбата (избира се обект "Ресторант-градина", за който са въведени първоначални примерни данни за продукти и рецепти към тях). След което на екрана се появяват номерираните маси на съответният обект. Избира се масата към която ще се маркират продажбите. Може да се отваря нова сметка или да се избере вече отворена такава от списъка със отворени сметки към тази маса (Когато към масата има отворена сметка тя е оцветена в червено ако няма отворена сметка масата е оцветена в зелено). Като се отвори сметката отляво се виждат продуктите за продажба с единичните си цени в този обект а отдясно празно поле на текущата поръчка.
Маркирането става като се избере продукт от списъка вляво, под полето се въвежда количеството и от бутона със стрелка надясно се добавя в полето на поръчката съотетното количество от избрания продукт. След добавяне на избраните продукти от бутон "Приключи поръчка" се финализира маркирането на конкретната поръчка.
При грешно добавен продукт в поръчката може да се премахне, като се избере сгрешеният продукт и се натисне бутона от долу със стрелка наляво.
Когато изберем сметка от списъка на отворените сметки на дадена маса се отваря прозорец с детайлите на сметката. От тук може да направим нова поръчка към тази сметка или да я приключим. При приключване се избира начин на плащане и се приключва сметката.

  Падащо меню "Продукция".

Тук се въвеждат продуктите за продажба със съответните си тип, единична цена и рецепта към този продукт.
Ако продуктът не фигурира в базата първо трябва да се добави от "Добави продукт". От меню "Добави продукт в обект" се добавя даден продукт в избран обект със съответната продажна цена за дадения обект. В различните обекти един продукт може да се предлага на различна цена. 
Зада се приспадат от складовете при продажбата на даден продукт съответните количества материали, съдържащите се продукта,  за всеки продукт в даден обект трябва да се въведат съответните рецепти с количествата материали от меню "Добави рецепта". За да може да се въведе нова рецепта трябва всички материали съдържащи се в продукта, да ги има като наименования в склада от който ще се изразходват(Ако няма рецепта за продукта в даден обект при продажбата няма да се приспадат количества от складовете!) 
