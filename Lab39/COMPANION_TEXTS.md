# Companion texts for Lab39 (copy/paste into GitHub UI)

## Issues (copy each as a new issue)

### Issue 1 — Add CSV export for order history (label: enhancement)
Title: Add CSV export for order history

Body:
```
Опис:
Додати можливість експортувати замовлення у CSV формат.

Acceptance Criteria:
- [ ] Реалізовано CsvExportService
- [ ] Метод повертає коректний CSV рядок
- [ ] Написано unit‑тест
```

### Issue 2 — Add pagination to product list (label: enhancement)
Title: Add pagination to product list

Body:
```
Опис:
Додати підтримку пагінації для списків сутностей.

Acceptance Criteria:
- [ ] Реалізовано PaginationService<T>
- [ ] Повертає PagedResult з TotalPages
- [ ] Написано unit‑тест
```

### Issue 3 — NullReferenceException in OrderService.GetById (label: bug)
Title: NullReferenceException in OrderService.GetById

Body:
```
Опис:
При запиті неіснуючого Id метод кидає NullReferenceException замість повернення null.

Кроки відтворення:
1. Викликати GetById з неіснуючим Id
2. Отримати NullReferenceException

Acceptance Criteria:
- [ ] Метод повертає null при відсутності запису
- [ ] Додано тест
```

### Issue 4 — Extract IOrderRepository interface (label: refactor)
Title: Extract IOrderRepository interface

Body:
```
Опис:
Виокремити інтерфейс IOrderRepository для дотримання DIP.

Acceptance Criteria:
- [ ] Створено IOrderRepository
- [ ] OrderRepository реалізує інтерфейс
- [ ] Залежності в коді через інтерфейс
```

## PR bodies (copy into PR description)

### PR for feature/add-pagination
```
## Summary
Додано сервіс пагінації для списків.

## Related Issue
Closes #2

## Changes
- `Lab39/Services/PaginationService.cs` — генерик‑сервіс PagedResult

## Checklist
- [x] Код компілюється без warnings
- [x] Тести проходять
- [x] Commit messages відповідають Conventional Commits
```

### PR for conflict/branch-b
```
## Summary
Вирішено конфлікт і додано комбіновану версію GetStatus.

## Related Issue
Closes #6

## Changes
- `Lab39/Services/ConflictDemo.cs` — GetStatus повертає "Active with logging and caching"

## Checklist
- [x] Код компілюється без warnings
- [x] Тести проходять
- [x] Commit messages відповідають Conventional Commits
```

### PR #2 (fix/null-reference-order-service) body
```
## Summary
Виправлено NullReferenceException в OrderService.GetById.

## Related Issue
Closes #3

## Changes
- `Lab39/Services/OrderService.cs` — GetById повертає null замість throw

## Checklist
- [x] Код компілюється без warnings
- [x] Тести проходять
- [x] Commit messages відповідають Conventional Commits
```

### PR #3 (refactor/extract-order-repository) body
```
## Summary
Виокремлено IOrderRepository для дотримання принципу DIP.

## Related Issue
Closes #4

## Changes
- `Lab39/Services/IOrderRepository.cs` — інтерфейс
- `Lab39/Services/OrderRepository.cs` — реалізація

## Checklist
- [x] Код компілюється без warnings
- [x] Тести проходять
- [x] Commit messages відповідають Conventional Commits
```

## Example review comments (copy to Files changed → comment)

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
