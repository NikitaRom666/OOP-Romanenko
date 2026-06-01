# Contributing Guide

## Branching Strategy

Використовуємо GitHub Flow:
- `main` -- завжди стабільний, прямі push заборонені
- Для кожної зміни -- окрема гілка від `main`

Формат назви гілки:
  feature/short-description
  fix/short-description
  refactor/short-description
  docs/short-description

## Commit Conventions

Дотримуємось Conventional Commits (https://www.conventionalcommits.org):

| Тип       | Призначення                       |
|-----------|-----------------------------------|
| feat:     | Нова функціональність             |
| fix:      | Виправлення бага                  |
| refactor: | Рефакторинг без зміни поведінки   |
| docs:     | Зміни в документації              |
| test:     | Тести                             |
| chore:    | Налаштування, залежності          |

Приклад: feat: add CSV export service for order history

## Як створити PR

1. git checkout main && git pull origin main
2. git checkout -b feature/your-feature
3. Зроби зміни, закомітуй.
4. git push -u origin feature/your-feature
5. На GitHub -- New pull request -- заповни шаблон.
6. Додай Closes #N на відповідний Issue.
7. Призначте рецензента і дочекайся Approve.

## Code Review

Під час review:
- suggestion -- конкретна пропозиція покращення коду
- question -- питання щодо рішення або підходу
- nitpick -- дрібне зауваження щодо стилю або іменування

Завершуй Review з Approve / Request Changes / Comment.

## Вирішення конфліктів

  git checkout your-branch
  git merge main
  # Відредагуй конфліктні файли, видали маркери <<<<, ====, >>>>
  git add .
  git commit -m "fix: resolve merge conflict in FileName"
  git push

Ніколи не залишай маркери конфлікту в коді.
