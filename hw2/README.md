# Приклади порушення LSP (Liskov Substitution Principle)

Цей документ описує два приклади порушення принципу підстановки Лісков, окрім класичного "Квадрат vs Прямокутник".

## 1. Проблема "Птах, що не літає" (The Ostrich Problem)

### Опис порушення
У нас є базовий клас `Bird` (Птах) з методом `fly()` (літати). Ми створюємо клас `Penguin` (Пінгвін) або `Ostrich` (Страус), який успадковується від `Bird`. Оскільки пінгвіни не літають, у методі `fly()` ми змушені кидати виняток або нічого не робити.

```python
class Bird:
    def fly(self):
        print("I am flying")

class Penguin(Bird):
    def fly(self):
        raise Exception("Penguins cannot fly!") 
        ### Чому це порушує LSP
Клієнтський код, який працює зі списком `Bird`, очікує, що всі птахи можуть літати. Якщо ми передамо туди `Penguin`, програма впаде з помилкою. Спадкоємець не може замінити батька.

### Як виправити
Розділити птахів на тих, що літають, і тих, що бігають/плавають.

```python
class Bird:
    pass

class FlyingBird(Bird):
    def fly(self):
        pass

class Penguin(Bird):
    def swim(self):
        pass
        Опис порушення
Є базовий клас File з методами read() та write(). Ми створюємо клас-нащадок ReadOnlyFile. У методі write() він змушений кидати помилку, бо запис заборонено.
class File:
    def read(self): ...
    def write(self, data): ...

class ReadOnlyFile(File):
    def write(self, data):
        raise PermissionError("Cannot write to read-only file")
        Чому це порушує LSP
Якщо функція очікує File і намагається записати дані, підстановка ReadOnlyFile зламає програму.

Як виправити
Використати розділення інтерфейсів.
class IReadable:
    def read(self): ...

class IWritable:
    def write(self, data): ...

class ReadOnlyFile(IReadable):
    # Тільки читання
    pass