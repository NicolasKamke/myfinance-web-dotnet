-- Inserindo dados na tabela type
INSERT INTO account_type (short_name, full_name, description)
VALUES 
    ('I', 'Income', 'Representa entradas de dinheiro'),
    ('E', 'Expense', 'Representa saídas de dinheiro');

-- Inserindo dados na tabela account_plan
INSERT INTO account_plan (name, account_type_id)
VALUES
    ('Salário', 1),       -- Income
    ('Investimentos', 1), -- Income
    ('Aluguel', 2),       -- Expense
    ('Compras', 2),       -- Expense
    ('Transporte', 2);    -- Expense

-- Inserindo dados na tabela transactions
INSERT INTO account_transaction (date_time, value, description, account_plan_id)
VALUES
    -- Transações de entrada (Income)
    ('2024-11-01 08:00:00', 5000.00, 'Salário de novembro', 1),
    ('2024-11-03 10:00:00', 300.00, 'Rendimento de investimentos', 2),

    -- Transações de saída (Expense)
    ('2024-11-05 15:30:00', 1500.00, 'Pagamento do aluguel', 3),
    ('2024-11-10 18:45:00', 200.00, 'Compras no supermercado', 4),
    ('2024-11-15 07:20:00', 50.00, 'Transporte para o trabalho', 5),
    ('2024-11-18 14:00:00', 120.00, 'Roupas', 4),
    ('2024-11-20 09:00:00', 80.00, 'Táxi', 5);
