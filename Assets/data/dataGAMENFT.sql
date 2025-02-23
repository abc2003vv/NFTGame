CREATE DATABASE IF NOT EXISTS GameNFT;
USE GameNFT;

-- Bảng lưu thông tin user
CREATE TABLE IF NOT EXISTS users (
    id INT AUTO_INCREMENT PRIMARY KEY,
    gmail VARCHAR(255) UNIQUE NOT NULL,
    username VARCHAR(255) NOT NULL,  -- Thêm cột username để lưu tên nhân vật
    wallet_address VARCHAR(255) UNIQUE,
    coins DECIMAL(18,8) DEFAULT 0.0,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Bảng lưu lịch sử giao dịch
CREATE TABLE IF NOT EXISTS transactions (
    id INT AUTO_INCREMENT PRIMARY KEY,
    user_id INT,
    tx_hash VARCHAR(255) UNIQUE NOT NULL,
    amount DECIMAL(18,8) NOT NULL,
    tx_type ENUM('deposit', 'withdraw') NOT NULL,
    status ENUM('pending', 'confirmed', 'failed') DEFAULT 'pending',
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE
);

-- Thêm một số user mẫu
INSERT INTO users (gmail, username, wallet_address, coins) VALUES
('user1@gmail.com', 'user1_character', '0x123abc...', 200.00),
('user2@gmail.com', 'user2_character', '0x456def...', 150.00);

-- Thêm một số giao dịch mẫu
INSERT INTO transactions (user_id, tx_hash, amount, tx_type, status) VALUES
(1, '0xabc123...', 50.00, 'deposit', 'confirmed'),
(2, '0xdef456...', 100.00, 'withdraw', 'pending');


ALTER TABLE users ADD COLUMN diamonds DECIMAL(18,8) DEFAULT 0.0;

-- Cập nhật dữ liệu mẫu
UPDATE users SET diamonds = 50.00 WHERE gmail = 'user1@gmail.com';
UPDATE users SET diamonds = 30.00 WHERE gmail = 'user2@gmail.com';


ALTER USER 'root'@'%' IDENTIFIED WITH mysql_native_password BY '123456';
FLUSH PRIVILEGES;

SHOW PLUGINS;

ALTER USER 'root'@'%' IDENTIFIED WITH caching_sha2_password BY '123456';
FLUSH PRIVILEGES;
