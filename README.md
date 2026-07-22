# 📚 Library Management System (C# WinForms + Data Structures & Algorithms + OOP)

A desktop application built with **C# (Windows Forms)** and **SQL Server** that demonstrates practical **Object-Oriented Programming (OOP)** alongside custom **Data Structures & Algorithms (DSA)** for efficient data manipulation in memory.

---

## ⚡ Data Structures & Algorithms (DSA) Implementation

Unlike typical CRUD apps that rely solely on standard framework collections or raw database queries, this system implements custom, low-level data structures to manage in-memory data operations:

* **Custom Linked Lists:**
  * `BookLinkedList.cs` & `LinkedList_IUD.cs`: Custom linked list implementations for dynamically tracking and manipulating book collections.
  * `StudentLinkedList.cs` & `StudentNode.cs`: Pointer-based node structures managing dynamic student records.
* **Custom Stack (`CustomStack.cs`):**
  * Implements LIFO (Last-In, First-Out) operations to handle action undo history, recent transactions, or book circulation tracking.
* **In-Memory CRUD Algorithms:**
  * Efficient Insertion, Update, and Deletion (`Student_IUD.cs`, `LinkedList_IUD.cs`) directly operating on custom node structures before/during database persistence.

---

## 🧩 OOP Concepts Applied

* **Classes & Encapsulation:** Core entity definitions (`BOOK.cs`, `Student.cs`, `StudentNode.cs`, `bookIssueDetail.cs`) protecting internal fields using C# properties.
* **Inheritance & Hierarchy:** Categorized book types (`Physics.cs`, `Mathematics.cs`, `OOP.cs`, `Marketing.cs`) extending base models (`BookShelf.cs`, `OtherBooks.cs`).
* **Abstraction:** Hiding complex pointer manipulation and database handling (`Database.cs`) behind simple form-action interfaces.

---

## 🌟 Application Features

* **Dashboard Navigation:** Central interface (`Dashboard.cs`) connecting all modules.
* **Book & Student Management:** Complete UI forms to add, view, and update entries (`AddBook.cs`, `ViewBook.cs`, `AddStudent.cs`, `ViewStudent.cs`).
* **Circulation Management:** Issue and process book returns seamlessly (`IssueBook.cs`, `ReturnBook.cs`).
* **Transaction Records:** Detailed history log views for all issued items (`CompleteBDetail.cs`).
* **SQL Persistence:** Script included for backend initialization (`OOP Project SQL.sql`).

---

## 🛠️ Built With

* **Language:** C#
* **UI Framework:** Windows Forms (WinForms)
* **Database:** Microsoft SQL Server
* **IDE:** Visual Studio

---

## 🚀 Setup & Installation

1. **Clone the Repository:**
   ```bash
   git clone [https://github.com/FASIHA-SE/LibarayManagementSystem-Csharp-DSA.git](https://github.com/FASIHA-SE/LibarayManagementSystem-Csharp-DSA.git)
