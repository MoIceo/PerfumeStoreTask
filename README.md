Минималистичный гайд
1) Использовать DataBaseScript.SQL для создания таблиц с данными.
2) Обновить строку подключения к серверу. Путь: StoreLibrary -> Data -> AppDbContext.cs  (....UseSqlServer("Server=ВАШ_СЕРВЕР;Database=FinalTask;Trusted_Connection=True; Trust Server Certificate=True");)
3) Обновить строку подключения во всех сервисах библиотеки. Путь: StoreLibrary -> Services -> ... (..._baseUrl = "http://localhost:ВАШ_ПОРТ/api/";)
4) Убедиться что проект запускает WebApi и Desktop.
5) Наслаждаться работоспособностью?

Великие разработчики сия чуда:
Горелкин Андрей, Туйкова Анна, Зайцев Матвей
