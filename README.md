# 📚 Hệ thống quản lý kinh phí & phân bổ sách

## 🚀 Giới thiệu
Hệ thống hỗ trợ quản lý quá trình **nhập sách và phân bổ sách** cho các đơn vị dựa trên **kinh phí được cấp hàng năm**.  
Mục tiêu:
- Đảm bảo phân bổ công bằng, minh bạch, theo tỷ lệ kinh phí còn lại của từng đơn vị.
- Giảm thiểu tính toán thủ công, hỗ trợ báo cáo nhanh và chính xác.
- Cho phép mở rộng theo nhiều năm, nhiều đợt, nhiều đơn vị.

---

## 🏢 Quy trình nghiệp vụ

1. **Khởi tạo đầu năm**
   - Nhập danh sách các đơn vị.
   - Gán kinh phí ban đầu cho từng đơn vị trong năm làm việc.

2. **Nhập sách theo đợt**
   - Mỗi năm có nhiều đợt mua sách.
   - Một đợt nhập thường gồm ~20 đầu sách.
   - Thông tin ghi nhận: mã số sách, tên, đơn vị tính, giá, số lượng.

3. **Phân bổ sách**
   - Khi đợt nhập đủ số lượng sách, hệ thống phân bổ về các đơn vị.
   - Nguyên tắc:
     - Tất cả đơn vị đều nhận sách.
     - Số lượng phân bổ dựa theo **tỷ lệ kinh phí còn lại**.
     - Phần dư được làm tròn và ưu tiên cho đơn vị có tỷ lệ cao hơn.

4. **Cập nhật ngân sách**
   - Giá trị sách được trừ vào kinh phí còn lại của đơn vị.
   - Ngân sách còn lại sẽ được dùng làm cơ sở cho các đợt phân bổ sau.

5. **Theo dõi & Báo cáo**
   - Báo cáo theo đợt: chi tiết phân bổ từng đầu sách cho từng đơn vị.
   - Báo cáo theo năm: tổng hợp số sách và kinh phí đã cấp cho từng đơn vị.
   - Xuất báo cáo dưới dạng Excel.

---

## 🗄️ Thiết kế cơ sở dữ liệu

### Thực thể chính
- **WorkYear**: năm làm việc (2025, 2026, …).
- **Unit**: đơn vị (trường, phòng, thư viện...).
- **UnitBudget**: kinh phí của đơn vị trong năm.
- **Batch**: đợt nhập sách.
- **BookBatch**: sách trong một đợt.
- **Allocation**: kết quả phân bổ sách cho đơn vị.
- **User**: tài khoản người dùng hệ thống.

### Quan hệ
```
WorkYear ---< UnitBudget >--- Unit
     |
     +---< Batch ---< BookBatch >--- Allocation >--- UnitBudget
```

### Mô tả bảng chính

#### WorkYear
- WorkYearId (PK, varchar(50))
- Year (int)
- Description (nvarchar)
- CreatedAt (datetime)

#### Unit
- UnitId (PK, varchar(50))
- UnitCode (nvarchar, unique)
- UnitName (nvarchar)
- Address, ContactPerson, Phone
- CreatedAt (datetime)

#### UnitBudget
- UnitBudgetId (PK, varchar(50))
- UnitId (FK → Unit)
- WorkYearId (FK → WorkYear)
- InitialBudget, RemainingBudget (decimal)
- CreatedAt (datetime)
- Ràng buộc: **UnitId + WorkYearId UNIQUE**

#### Batch
- BatchId (PK, varchar(50))
- WorkYearId (FK → WorkYear)
- BatchName (nvarchar)
- CreatedDate (datetime)

#### BookBatch
- BookBatchId (PK, varchar(50))
- BatchId (FK → Batch)
- BookCode (nvarchar)
- Title (nvarchar)
- UnitOfMeasure (nvarchar, default 'cuốn')
- Price (decimal)
- Quantity (int)

#### Allocation
- AllocationId (PK, varchar(50))
- BatchId (FK → Batch)
- BookBatchId (FK → BookBatch)
- UnitBudgetId (FK → UnitBudget)
- AllocatedQuantity (int)
- AllocatedCost (decimal)
- CreatedAt (datetime)

#### User
- UserId (PK, varchar(50))
- Username (unique)
- PasswordHash
- FullName
- Role (admin, manager, viewer)
- CreatedAt
- IsActive

---

## 📊 Ví dụ quy trình dữ liệu

- **Năm làm việc**: 2025
- **Đơn vị**:
  - Trường A: 100 triệu
  - Trường B: 50 triệu
- **Đợt nhập (Batch1)**:
  - Toán lớp 1: 100 cuốn × 10.000đ
  - Văn lớp 2: 80 cuốn × 20.000đ
- **Phân bổ (Allocation)**:
  - Toán lớp 1 → A: 60 cuốn (600.000đ), B: 40 cuốn (400.000đ)
  - Văn lớp 2 → A: 48 cuốn (960.000đ), B: 32 cuốn (640.000đ)
- **Cập nhật ngân sách**:
  - Trường A còn lại: 100 triệu – 1,56 triệu = 98,44 triệu
  - Trường B còn lại: 50 triệu – 1,04 triệu = 48,96 triệu

---

## 🔮 Hướng mở rộng
- **Bổ sung quản lý kho**: theo dõi tồn kho sách sau khi phân bổ.
- **Bổ sung audit log**: lưu lại lịch sử thay đổi kinh phí.
- **Phân quyền người dùng**: chi tiết hơn (nhập liệu, phê duyệt, báo cáo).
- **API mở rộng**: tích hợp với hệ thống quản lý thư viện hoặc giáo dục khác.

---

## 👨‍💻 Công nghệ khuyến nghị
- Backend: **ASP.NET Core + EF Core**
- Database: **SQL Server**
- Frontend: Razor Pages / MVC + Bootstrap
- Excel Import/Export: **EPPlus** hoặc **ClosedXML**
