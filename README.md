# HTML Parser Application

## 📖 Overview  
The HTML Parser Application is a simple and efficient tool for fetching, parsing, and analyzing HTML content from web pages. It is divided into three tabs: **Html**, **Result**, and **History**, each with distinct functionalities.

---

## 🚀 Features  

### **Html Tab**  
- **URL Input:** A `TextBox` for entering the target URL.  
- **Run Button:** Fetches HTML content from the URL and displays it in the output `TextBox`.  
- **Clear Button:** Clears the displayed HTML content.  
- **HTML Content Display:** A multi-line `TextBox` that shows the retrieved HTML.

### **Result Tab**  
- **Selector Input:** A `TextBox` for entering XPath or CSS selectors to parse the HTML.  
- **Selector Type:** A `ComboBox` to select the type of selector:  
  - `XPath`  
  - `CSSSelector`  
- **Search Button:** Parses the HTML using the selected type and displays the result in the `TextBox`.  
- **Clear Result Button:** Clears the parsed result.

### **History Tab**  
- **History Table:** Displays all parse operations.  
- **Stored Columns:**  
  - **Id:** Unique identifier.  
  - **TipoSeletor:** Type of selector used (`XPath` or `CSSSelector`).  
  - **Seletor:** The selector string.  
  - **Resultado:** The output from parsing.  
  - **Data:** Date and time of the parse operation.

---
### ✨ **Example**
Fetch HTML Content
- Enter https://uol.com.br/ in the URL input.
- Click Run to retrieve and display the HTML content in the output box.

 ![image](https://github.com/user-attachments/assets/65f1c43c-809d-4563-8ec7-4a0441c392b9)

Parse HTML Content
- Enter an XPath (e.g., //div[@class='example']) or a CSS selector (e.g., .example).
- Select the type (XPath or CSSSelector) from the dropdown.
- Click Search to display the parsed result in the Result Tab.

![image](https://github.com/user-attachments/assets/558b852c-a0d1-4f31-b6f1-e536e9b1a970)
![image](https://github.com/user-attachments/assets/ffc81ee2-62e4-4025-b7e7-dbbd43658591)

View History
Navigate to the History Tab and click in "Get History" to see a list of all previous parsing operations stored in the database.

![image](https://github.com/user-attachments/assets/f58a4373-83b3-4acc-9957-860106b094bc)
![image](https://github.com/user-attachments/assets/47641b2d-20c4-48a1-933f-b6a57af4373d)


## 🗄️ Database  

The application uses **SQLite** to manage and persist the parsing history.  

### **Database Table Schema**

```sql
CREATE TABLE IF NOT EXISTS Selectors (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    TipoSeletor TEXT,
    Seletor TEXT,
    Resultado TEXT,
    Data TEXT
);

