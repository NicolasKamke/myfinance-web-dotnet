-- Criação da tabela type
CREATE TABLE account_type (
    id INT PRIMARY KEY IDENTITY(1,1), -- Chave primária com incremento automático
    short_name VARCHAR(8) NOT NULL,       -- Tipo com limite de 8 caracteres
    full_name VARCHAR(40) NOT NULL,       -- Tipo com limite de 40 caracteres
    description VARCHAR(255)          -- Descrição opcional com até 255 caracteres
);

-- Criação da tabela account_plan
CREATE TABLE account_plan (
    id INT PRIMARY KEY IDENTITY(1,1), -- Chave primária com incremento automático
    name VARCHAR(255) NOT NULL,       -- Nome com limite de 255 caracteres
    account_type_id INT NOT NULL,             -- Chave estrangeira para a tabela type
    CONSTRAINT FK_account_plan_account_type FOREIGN KEY (account_type_id) REFERENCES account_type(id)
);

-- Criação da tabela transactions
CREATE TABLE account_transaction (
    id INT PRIMARY KEY IDENTITY(1,1), -- Chave primária com incremento automático
    date_time DATETIME NOT NULL,      -- Data e hora da transação
    value DECIMAL(19,4) NOT NULL,     -- Valor da transação, com precisão de 4 casas decimais
    description VARCHAR(500),         -- Descrição opcional com até 500 caracteres
    account_plan_id INT NOT NULL,     -- Chave estrangeira para a tabela account_plan
    CONSTRAINT FK_account_transactions_account_plan FOREIGN KEY (account_plan_id) REFERENCES account_plan(id)
);
