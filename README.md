# MongoServerTask3
MongoServerTask3

# API
## items.count
Возвращает количество товаров указанного типа, возвращает количество типов, если item_id не указан.
### METHOD: 
GET
### PARAMS: 
item_id(string, nullable)
### RETURN:
```
{
	“count”: 123
}
```
### ERRORS:
- item_id невалидный
## items.get
Возвращает список товаров, полученных по фильтру. Если фильтр указан, а параметр — нет, фильтр игнорируется. Если параметр указан, а фильтр — нет, фильтру присваивается значение 0. 
### METHOD: 
GET
### PARAMS: 
item_id (string, nullable) name (string,nullable), price_filter (enum: 0 equal, 1 greater, 2 less) (nullable), price (long, nullable), count_filter (enum: 0 equal, 1 greater, 2 less) (nullable), count (int, nullable), page (int, nullable), page_size (int, nullable)
### RETURN:
array
```
[{
	“id”: “askjf10930”,
	“name”: “some_name”,
	“price”: 123123,
	“count”: 123,
	“company”: “company_name”
}]
```
### ERRORS:
- номер страницы выходит за пределы массива
-. размер страницы не указан, в то время, как номер страницы указан
- неправильный тип данных параметра
## items.create
Создает или добавляет товар в категорию (если категория указана)
### METHOD: 
POST
### PARAMS: 
NONE
### POST OBJECT:
```
{
	“name”: “some_name”,
	“price”: 123123,
	“count”: 123,
	“company”: “company_name”
}
```
### RETURN:
```
{
	“status”: (enum: 0 created, 1 created in category, 2 added, 3 error)
	“item_id”: “g1yu23t87213giug”
}
```

## items.delete
Удаляет товар из базы данных
### METHOD: 
DELETE
### PARAMS: 
item_id
### RETURN:
```
{
	“status”: (enum: 0 not find, 1 success)
}
```
### ERRORS:
- item_id не указан
## employee.count
Получить количество работников
### METHOD: 
GET
### PARAMS: 
NONE
### RETURN:
```
{
	“count”: 123
}
```
## employee.get
Получить работников
### METHOD: 
GET
### PARAMS: 
employee_id (string, nullable), name (string, nullable), salary_filter (enum: 0 equal, 1 greater, 2 less), salary (int, nullable), phone (string, nullable), email (string, nullable), page (int, nullable), page_size (int, nullable)
### RETURN:
```
[{
	“id”: “sadfasdfa09”,
	“name”: “john doe”,
	“salary”: 123123,
	“address”: “street 1”,
	“phone”: “88005553535”,
	“email”: “example@mail.com”
}]
```
### ERRORS:
- номер страницы выходит за пределы массива
- размер страницы не указан, в то время, как номер страницы указан
- неправильный тип данных параметра
- Слишком большое значение page_size
## employee.create
### METHOD: 
POST
### PARAMS: 
NONE
### POST OBJECT:
```
{
	“name”: “john doe”,
	“position”: “director”,
	“salary”: 123123,
	“password_data”: 123434,
	“address”: “street 1”,
	“phone”: “88005553535”,
	“email”: “example@mail.com”
}
```
## employee.delete
### METHOD: 
DELETE
### PARAMS: 
id (string)
### RETURN:
```
{
	“status”: (enum: 0 not find, 1 success)
}
```
### ERRORS:
- employee_id не указан
## Коды ошибок и описания
    1. Invalid id – id не указан или имеет неправильный формат
    2. Номер страницы выходит за пределы массива
    3. Размер страницы не указан, в то время, как номер страницы указан
    4. Неправильный тип данных параметра
    5. Неизвестная ошибка
    6. Нет доступа
    7. Тело запроса не должно быть пустым
    8. Неправильный формат тела запроса
    9. Слишком большое значение page_size
