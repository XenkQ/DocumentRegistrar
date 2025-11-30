# DocumentRegistrar

## Overview

Learning project to create a modern Windows desktop application built with **WinUI 3** and **.NET 9**. The application provides a user-friendly interface for managing contractors, admission documents, and document positions. It leverages the MVVM pattern, CommunityToolkit, and a modular service-based architecture for maintainability and scalability.

---

## Features

- **Contractor Management**:  
  - Create, edit, and view contractors.
  - Validation and error handling for contractor data.

- **Admission Document Management**:  
  - Create, edit, and view admission documents.
  - Assign contractors to documents.
  - Date and symbol management.

- **Document Position Management**:  
  - Add, edit, and view positions within admission documents.
  - Product details, measurement units, and quantities.

- **Navigation & Dialog Services**:  
  - Seamless navigation between pages.
  - User feedback via dialog messages.

- **Modern UI**:  
  - Built with WinUI 3 for a native Windows 10/11 look and feel.
  - Responsive layouts and custom controls.

---

## Technologies Used
- **.NET 9**
- **Auto Mapper**
- **Microsoft SQL Server**

### Backend
- **.NET Core**
- **Open API** with **Scalar**
- **Entity Framework** with MS SQL SERVER connection

### Frontend
- **WinUI 3**
- **CommunityToolkit.Mvvm**
---

## Getting Started

### Prerequisites

- Windows 10 version 17763 or later
- Visual Studio 2022 (latest recommended)
- .NET 9 SDK
- Microsoft SQL Server 2022 Express

### Build & Run

1. **Clone the repository**

2. **Open the solution in Visual Studio 2022**

3. **Restore NuGet packages**  
   Visual Studio will restore packages automatically on build.

4. **Build solution**
   Press `Ctrl + shift + b` to build entire solution.

5. **Edit db connection string in backend**
  `appsettings.json` attribute `ConnectionStrings > DefaultConnection`

6. **Run Backend without debug mode**

7. **Set `Frontend` as the startup project**

8. **Build and run**  
   Press `F5` to build and launch the application.

## Screenshots (Polish version)

### Admin Panel
<img width="1417" height="737" alt="AdminPanel" src="https://github.com/user-attachments/assets/01967330-b21b-4768-a6ff-49956fa56bd0" />

### Manager Panel
<img width="1415" height="733" alt="ManagerMainMenu" src="https://github.com/user-attachments/assets/cd4eadd1-f43e-4383-ac79-66155aff4d74" />

### User Panel
<img width="1413" height="734" alt="UserPanel" src="https://github.com/user-attachments/assets/6da2478e-7ee6-4e8b-87e0-e76483ee274f" />

### Contrahents table view
<img width="1411" height="732" alt="KontrahenciListView" src="https://github.com/user-attachments/assets/6f888bf0-d6b1-41ac-9aba-4e2ab2942be6" />

### Contrahent edit / add view
<img width="1415" height="734" alt="EdycjaKontrahenta" src="https://github.com/user-attachments/assets/0e413420-3c19-49f4-befe-c889641bcbfa" />

### Admission Documents table view
<img width="1412" height="730" alt="AdmissionDocumentsListView" src="https://github.com/user-attachments/assets/b5ae45aa-bc7b-4b59-9587-8a81c8a42496" />

### Admission Document edit / add view
<img width="1412" height="735" alt="EdycjaDokumentuPrzyjecia" src="https://github.com/user-attachments/assets/73d6971e-5600-4515-9d8d-a63f7e453fa3" />

### Position Documents under Admission Document table view
<img width="1417" height="733" alt="DocumentPositionsUnderAdmissionDocumentListView" src="https://github.com/user-attachments/assets/fecafd2f-1c61-4037-b5cc-6ef4b83bb81c" />

### Position Documents edit / add view
<img width="1415" height="735" alt="EdycjaPozycjiDokumentu" src="https://github.com/user-attachments/assets/49e912bb-9db4-45a2-bce4-4e2b9f84f7ee" />

### Login Panel
<img width="1415" height="734" alt="PanelLogowania" src="https://github.com/user-attachments/assets/a31098e0-21a4-4488-903e-ae8afacf10b2" />
