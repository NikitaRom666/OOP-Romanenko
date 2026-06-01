# Тексти для Lab39 (скопіюйте/вставте у GitHub UI)

## Issues (створіть кожен як новий Issue)

### Issue 1 — Додати CSV-експорт історії замовлень (label: enhancement)
Title: Додати CSV-експорт історії замовлень

Body:
```
Опис:
Додати можливість експортувати замовлення у CSV формат.

Критерії прийняття:
- [ ] Реалізовано CsvExportService
- [ ] Метод повертає коректний CSV рядок
- [ ] Написано unit‑тест
```

### Issue 2 — Додати пагінацію для списку продуктів (label: enhancement)
Title: Додати пагінацію для списку продуктів

Body:
```
Опис:
Додати підтримку пагінації для списків сутностей.

Критерії прийняття:
- [ ] Реалізовано PaginationService<T>
- [ ] Повертає PagedResult з TotalPages
- [ ] Написано unit‑тест
```

### Issue 3 — NullReferenceException в OrderService.GetById (label: bug)
Title: NullReferenceException в OrderService.GetById

Body:
```
Опис:
При запиті неіснуючого Id метод кидає NullReferenceException замість повернення null.

Кроки відтворення:
1. Викликати GetById з неіснуючим Id
2. Отримати NullReferenceException

Критерії прийняття:
- [ ] Метод повертає null при відсутності запису
- [ ] Додано тест
```

### Issue 4 — Виокремити інтерфейс IOrderRepository (label: refactor)
Title: Виокремити інтерфейс IOrderRepository

Body:
```
Опис:
Виокремити інтерфейс IOrderRepository для дотримання DIP.

Критерії прийняття:
- [ ] Створено IOrderRepository
- [ ] OrderRepository реалізує інтерфейс
- [ ] Залежності в коді через інтерфейс
```

## Тіла для PR (вставте у поле опису PR)

### PR для feature/add-pagination
```
## Коротко
Додано сервіс пагінації для списків.

## Пов'язане Issue
Closes #2

## Зміни
- `Lab39/Services/PaginationService.cs` — генерик‑сервіс PagedResult

## Чек‑лист
- [x] Код компілюється без warnings
- [x] Тести проходять
- [x] Commit messages відповідають Conventional Commits
```

### PR для conflict/branch-b
```
## Коротко
Вирішено конфлікт і додано комбіновану версію GetStatus.

## Пов'язане Issue
Closes #6

## Зміни
- `Lab39/Services/ConflictDemo.cs` — GetStatus повертає "Active with logging and caching"

## Чек‑лист
- [x] Код компілюється без warnings
- [x] Тести проходять
- [x] Commit messages відповідають Conventional Commits
```

### PR #2 (fix/null-reference-order-service) — опис
```
## Коротко
Виправлено NullReferenceException в OrderService.GetById.

## Пов'язане Issue
Closes #3

## Зміни
- `Lab39/Services/OrderService.cs` — GetById повертає null замість throw

## Чек‑лист
- [x] Код компілюється без warnings
- [x] Тести проходять
- [x] Commit messages відповідають Conventional Commits
```

### PR #3 (refactor/extract-order-repository) — опис
```
## Коротко
Виокремлено IOrderRepository для дотримання принципу DIP.

## Пов'язане Issue
Closes #4

## Зміни
- `Lab39/Services/IOrderRepository.cs` — інтерфейс
- `Lab39/Services/OrderRepository.cs` — реалізація

## Чек‑лист
- [x] Код компілюється без warnings
- [x] Тести проходять
- [x] Commit messages відповідають Conventional Commits
```

## Приклади коментарів для рев'ю (скопіюйте у Files changed → comment)

Suggestion:
```
**suggestion**: Метод робить і валідацію і збереження. Краще розділити:

```suggestion
public ValidationResult Validate(Order order) { ... }
public async Task SaveAsync(Order order) { ... }
```
```

Question:
```
**question**: Чому тут `List<T>` замість `IEnumerable<T>` у параметрі? Чи є причина фіксувати конкретний тип колекції?
```

Nitpick:
```
**nitpick**: Змінна `d` не описова, краще назвати `orderDate` або `createdAt`.
```
