const express = require("express");
const mysql = require("mysql2");
const cors = require("cors");
const bodyParser = require("body-parser");

const app = express();
app.use(cors());
app.use(bodyParser.json());

// Kết nối MySQL
const db = mysql.createConnection({
  host: "localhost", // Hoặc IP của máy chủ MySQL
  user: "root", // Username MySQL của bạn
  password: "123456", // Password MySQL của bạn
  database: "GameNFT", // Tên database
  port: 3306, // Cổng MySQL mặc định
});

db.connect((err) => {
  if (err) {
    console.error("❌ Lỗi kết nối MySQL:", err.message);
  } else {
    console.log("✅ Kết nối MySQL thành công!");
  }
});

// 🚀 API Lấy danh sách user
app.get("/users", (req, res) => {
  db.query("SELECT * FROM users", (err, results) => {
    if (err) return res.status(500).json({ error: err.message });
    res.json(results);
  });
});

// 🚀 API Lấy danh sách giao dịch
app.get("/transactions", (req, res) => {
  db.query("SELECT * FROM transactions", (err, results) => {
    if (err) return res.status(500).json({ error: err.message });
    res.json(results);
  });
});

app.post("/users/login", (req, res) => {
    const { gmail } = req.body;

    const sql = "SELECT id, gmail, username, coins, diamonds FROM users WHERE gmail = ?";
    db.query(sql, [gmail], (err, result) => {
        if (err) {
            res.status(500).json({ success: false, error: "Lỗi truy vấn database" });
            return;
        }
        if (result.length > 0) {
            res.json({
                success: true,
                data: {
                    id: result[0].id,
                    gmail: result[0].gmail,
                    username: result[0].username,
                    coins: result[0].coins,
                    diamonds: result[0].diamonds
                }
            });
        } else {
            res.status(404).json({ success: false, error: "Không tìm thấy user" });
        }
    });
});


// 🚀 API Ghi nhận giao dịch
app.post("/transactions", (req, res) => {
  const { user_id, tx_hash, amount, tx_type } = req.body;
  db.query(
    "INSERT INTO transactions (user_id, tx_hash, amount, tx_type) VALUES (?, ?, ?, ?)",
    [user_id, tx_hash, amount, tx_type],
    (err, result) => {
      if (err) return res.status(500).json({ error: err.message });
      res.json({ status: "success", message: "Giao dịch đã lưu!", id: result.insertId });
    }
  );
});

// 🚀 API Đăng ký user
app.post("/users", (req, res) => {
  const { gmail, wallet_address, coins } = req.body;  // Không có password ở đây
  db.query(
    "INSERT INTO users (gmail, wallet_address, coins) VALUES (?, ?, ?)",
    [gmail, wallet_address, coins],  // Không cần password
    (err, result) => {
      if (err) return res.status(500).json({ error: err.message });
      res.json({ status: "success", message: "Thêm user thành công!", id: result.insertId });
    }
  );
});

// 🚀 API Đăng nhập user (Chỉ kiểm tra email)
app.post("/users/login", (req, res) => {
  const { gmail } = req.body; // Chỉ lấy email
  db.query(
    "SELECT * FROM users WHERE gmail = ?",
    [gmail],  // Chỉ kiểm tra email
    (err, results) => {
      if (err) return res.status(500).json({ error: err.message });
      if (results.length > 0) {
        res.json({ status: "success", message: "Đăng nhập thành công!", user: results[0] });
      } else {
        res.status(404).json({ status: "fail", message: "Không tìm thấy user với email này" });
      }
    }
  );
});

app.get("/", (req, res) => {
  res.send("API is working!");
});



// Chạy server
const PORT = 3000;
app.listen(PORT, () => {
  console.log(`✅ Server đang chạy tại http://localhost:${PORT}`);
});
