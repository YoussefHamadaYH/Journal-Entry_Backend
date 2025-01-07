# Journal Entry Task - Backend

This repository contains the backend implementation for a **Journal Entry Task** that focuses on documenting and archiving financial transactions. The backend is built using **ASP.NET** and integrates with a **SQL Server** database. Below is a detailed description of the backend functionality and technologies used.

---

## Technologies Used

- **Backend Framework**: ASP.NET WebApi
- **Database**: SQL Server
- **ORM**: Entity Framework 
- **AutoMapper**: For object-to-object mapping
- **Dependency Injection**: For repository and service management
---

## Backend Functionality

### 1. Journal Entry Header

- **Fields**:
  - **Entry Date** (Mandatory): The date of the transaction.
  - **Description** (Mandatory): A brief explanation of the transaction.
- **Validation**: Ensures mandatory fields are filled before saving to the database.

### 2. Journal Entry Details

- **Fields**:
  - **Debit Amount**: The amount to be debited.
  - **Credit Amount**: The amount to be credited.
  - **Account Name & Number**: Auto-populated based on user input.
  - **Account ID**: Stored in the database (not visible to the user).
- **Rules**:
  - If a value is entered in the **Debit** field, the **Credit** field must be zero, and vice versa.
  - The system ensures that the total **Debit** equals the total **Credit** before saving.



### 9. Database Structure

- **Tables**:
  - **JournalEntryHeader**: Stores the header information (Entry Date, Description, etc.).
  - **JournalEntryDetails**: Stores the details (Debit, Credit, Account ID, etc.).
  - **AccountsChart** : Stores account information used in journal entries.
---

## API Endpoints

### 1. **GET /api/GetAllJournals** : Retrieves all journal entries with their details.
### 2. **POST /api/CreateJournal** : Creates a new journal entry.
### 3. **GET /api/GetJournalById/{id}** : Retrieves a specific journal entry by its ID.
### 4. **GET /api/GetAllAccountNumberAndId** : Retrieves a list of account names and numbers for auto-complete functionality.

