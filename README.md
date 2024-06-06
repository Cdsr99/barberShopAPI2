# BarberShopAPI2 (Working on it)

Start the project running the follow commands:

```shell
dotnet add package Microsoft.EntityFrameworkCore --version 8.0.6
```

```shell
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.0.6
```

```shell
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```
```shell
dotnet add package Swashbuckle.AspNetCore.Annotations --version 6.6.2
```

## Database

```shell
create database db_barbershop;

use db_barbershop;

CREATE TABLE schedules (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
    date DATE NOT NULL,
    hour VARCHAR(255) NOT NULL,
    status VARCHAR(255) NOT NULL,
    created_at DATETIME NULL,
    updated_at DATETIME NULL
);

CREATE TABLE bookings (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
    client VARCHAR(255) NOT NULL,
    schedules_id BIGINT NOT NULL,
    status VARCHAR(255) NOT NULL DEFAULT 'Booked',
    created_at DATETIME NULL,
    updated_at DATETIME NULL,
    FOREIGN KEY (schedules_id) REFERENCES schedules(id)
);
```

Insert
```shell
-- Inserts for schedules table
INSERT INTO schedules (date, hour, status, created_at, updated_at) VALUES ('2024-06-01', '10:00', 'Available', GETDATE(), GETDATE());
INSERT INTO schedules (date, hour, status, created_at, updated_at) VALUES ('2024-06-01', '11:00', 'Booked', GETDATE(), GETDATE());
INSERT INTO schedules (date, hour, status, created_at, updated_at) VALUES ('2024-06-02', '09:00', 'Available', GETDATE(), GETDATE());
INSERT INTO schedules (date, hour, status, created_at, updated_at) VALUES ('2024-06-02', '10:00', 'Booked', GETDATE(), GETDATE());
INSERT INTO schedules (date, hour, status, created_at, updated_at) VALUES ('2024-06-03', '08:00', 'Available', GETDATE(), GETDATE());

-- Inserts for bookings table
INSERT INTO bookings (client, schedules_id, status, created_at, updated_at) VALUES ('John Doe', 1, 'Booked', GETDATE(), GETDATE());
INSERT INTO bookings (client, schedules_id, status, created_at, updated_at) VALUES ('Jane Smith', 2, 'Booked', GETDATE(), GETDATE());
INSERT INTO bookings (client, schedules_id, status, created_at, updated_at) VALUES ('Alice Johnson', 4, 'Booked', GETDATE(), GETDATE());
INSERT INTO bookings (client, schedules_id, status, created_at, updated_at) VALUES ('Bob Brown', 3, 'Booked', GETDATE(), GETDATE());
INSERT INTO bookings (client, schedules_id, status, created_at, updated_at) VALUES ('Charlie Davis', 5, 'Booked', GETDATE(), GETDATE());



insert into Settings (Parameter, Value) values ('Start','09:00:00');
insert into Settings (Parameter, Value) values ('End','19:00:00');
insert into Settings (Parameter, Value) values ('TimePerClient','00:30:00');
INSERT INTO Settings (Parameter, Value) VALUES ('DaysOff', '["Monday", "Tuesday"]');
```
```shell
dotnet add package Newtonsoft.Json
```
