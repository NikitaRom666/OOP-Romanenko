# Лабораторна робота №28 — Серіалізація об'єктів у JSON

## Мета
Розібратися, як беруться об'єкти з програми, зберігаються у файл як JSON і загружаються назад. Це майже стандартна задача, коли потрібно щось зберегти, щоб наступного разу не вводити всі дані заново.

## Що реалізовано
Я створив кілька класів для управління тренуваннями: `Exercise` це окрема вправа з назвою, групою м'язів, тривалістю і спаленими калоріями. `Workout` складається з кількох вправ і має назву, дату, тренера. `WorkoutRepository` це по суті контейнер, який зберігає список тренувань у пам'яті, може їх додавати, шукати по ID, і головне — зберігати цілий список у JSON файл або завантажити з нього. Це спрощена база даних без справжньої БД.

## Структура проєкту
```text
lab28v13/
  Exercise.cs
  MuscleGroup.cs
  Workout.cs
  WorkoutRepository.cs
  Program.cs
  workouts.json (генерується під час запуску)
```

## Як запустити
```
dotnet build
dotnet run
```

Програма створить два тренування, збереже їх у `workouts.json`, потім завантажить цей файл з нуля і виведе на екран інформацію про кожне тренування.

## Приклад виведення
```
Saved to workouts.json

Title: Morning Strength
Date: 25.03.2026
Trainer: Alex
Exercises: 3
Total duration: 3.5 minutes
Total calories: 35 kcal
  Push-up — Chest — 15 kcal
  Pull-up — Back — 12 kcal
  Plank — Core — 8 kcal

Title: Cardio Day
Date: 26.03.2026
Trainer: Maria
Exercises: 2
Total duration: 40.0 minutes
Total calories: 330 kcal
  Running — Cardio — 250 kcal
  Jump rope — Cardio — 80 kcal

GetById(1): Morning Strength
GetById(99): Not found
```

## Висновок
Насправді не так складно беруться об'єкти і перетворюються у JSON через `JsonSerializer`. Найцікавіше було те, що енум `MuscleGroup` можна було прямо у JSON записати як текст, не як число, завдяки `JsonStringEnumConverter`. Зрозумів, що такий підхід з файловою серіалізацією годиться для невеликих даних, а для більш серйозних проєктів краще справжня база.
