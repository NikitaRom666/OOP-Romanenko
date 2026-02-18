# Лабораторна робота №25
## Інтеграція патернів

### Опис

У цій роботі реалізовано інтегровану систему, що демонструє взаємодію між компонентами, які реалізують патерни Factory Method, Singleton, Strategy та Observer.

### Структура проєкту

#### Патерн Factory Method

- ILogger - інтерфейс логера
- ConsoleLogger - логування в консоль
- FileLogger - логування в файл
- LoggerFactory - абстрактна фабрика
- ConsoleLoggerFactory - фабрика для створення ConsoleLogger
- FileLoggerFactory - фабрика для створення FileLogger

#### Патерн Singleton

- LoggerManager - менеджер логерів (Singleton), дозволяє змінювати фабрику під час виконання

#### Патерн Strategy

- IDataProcessorStrategy - інтерфейс стратегії обробки даних
- EncryptDataStrategy - стратегія шифрування даних
- CompressDataStrategy - стратегія стиснення даних
- DataContext - контекст, який використовує стратегію обробки даних

#### Патерн Observer

- DataPublisher - клас-видавець з подією DataProcessed
- ProcessingLoggerObserver - спостерігач, який підписується на подію DataProcessed і використовує LoggerManager для логування

### Запуск

```
dotnet run
```

### Сценарії

#### Сценарій 1: Повна інтеграція

Ініціалізується LoggerManager з ConsoleLoggerFactory. Створюється DataContext з EncryptDataStrategy. Створюється DataPublisher. Створюється ProcessingLoggerObserver та підписується на подію DataProcessed. Виконується обробка даних через DataContext. Викликається PublishDataProcessed з DataPublisher.

#### Сценарій 2: Динамічна зміна логера

Після першої обробки змінюється фабрика в LoggerManager на FileLoggerFactory. Виконується обробка та публікація даних ще раз. Логування змінюється відповідно до нової фабрики.

#### Сценарій 3: Динамічна зміна стратегії

Після першої обробки змінюється стратегія в DataContext на CompressDataStrategy. Виконується обробка та публікація даних ще раз. Обробка даних відбувається за новою стратегією.

### Приклад виводу

```
Лабораторна робота №25: Інтеграція патернів

Сценарій 1: Повна інтеграція
----------------------------
Початкові дані: Hello World
Оброблені дані: SGVsbG8gV29ybGQ=
[ConsoleLogger] 2026-02-18 19:52:08 - Дані оброблено стратегією 'Encrypt': SGVsbG8gV29ybGQ=

Сценарій 2: Динамічна зміна логера
----------------------------
Початкові дані: Test Data for File Logger
Оброблені дані: VGVzdCBEYXRhIGZvciBGaWxlIExvZ2dlcg==
[FileLogger] Записано в файл: [FileLogger] 2026-02-18 19:52:08 - Дані оброблено стратегією 'Encrypt': VGVzdCBEYXRhIGZvciBGaWxlIExvZ2dlcg==

Сценарій 3: Динамічна зміна стратегії
----------------------------
Початкові дані: AAAABBBCCCCDDD
Оброблені дані: A4B3C4D3
[FileLogger] Записано в файл: [FileLogger] 2026-02-18 19:52:08 - Дані оброблено стратегією 'Compress': A4B3C4D3
```

### Технічні характеристики

- .NET 10.0
- C#
