# DocumentRegistrar

## Overview

This is a modern Windows desktop application built with **WinUI 3** and **.NET 9**. The application provides a user-friendly interface for managing contractors, admission documents, and document positions. It leverages the MVVM pattern, CommunityToolkit, and a modular service-based architecture for maintainability and scalability.

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

### Build & Run

1. **Clone the repository**

2. **Open the solution in Visual Studio 2022**

3. **Restore NuGet packages**  
   Visual Studio will restore packages automatically on build.

4. **Build solution**
   Press `Ctrl + shift + b` to build entire solution.

4. **Run Backend without debug mode**

5. **Set `Frontend` as the startup project**

6. **Build and run**  
   Press `F5` to build and launch the application.
