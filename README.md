# ToDo Project

The ToDo project is a simple web application designed to help users manage their tasks efficiently.Additionally, It includes APIs to create authentication tokens and retrieve all data with administrative role permissions. 

## Table of Contents
- [Introduction](#introduction)
- [Features](#features)
- [Getting Started](#getting-started)
- [API Endpoints](#api-endpoints)
- [Categories and Items Relationship](#categories-and-items-relationship)
- [Users and Roles](#users-and-roles)

## Introduction
The ToDo project is designed to help users manage their tasks by providing a set of APIs to interact with tasks (items) and categories. Users can create, read, update, and delete tasks and categories using the provided API endpoints. Additionally, the project includes authentication and authorization mechanisms to ensure secure access to the APIs and comes pre-seeded with differenet roles to serve differenet purposes. With the ToDo project, administrators gain insights using the Admin API, while normal users can easily manage their tasks with the category and item APIs, ensuring a seamless and personalized experience for all users.

## Features
1. Create, Read, Update, and Delete tasks (items) via CRUD APIs.
2. Create, Read, Update, and Delete categories via CRUD APIs.
3. Obtain an authentication token to access protected APIs.
4. Retrieve all data with administrative role permissions.

## Getting Started
To get the ToDo project up and running on your local machine, follow these steps:
1. Clone the repository:
```bash
git clone https://github.com/mhmd-da/todo.git
```
2. Set up the MongoDB connection in **appsettings.json** (or you can use the **docker-compose-mongo.yaml** file to run MongoDB locally using docker image)
3. Build and run the project

## API Endpoints
The following API endpoints are available in the ToDo project (you can export the postman colllection called **ToDo.postman_collection.json**):
- Category APIs:
  - GET /api/todo/category: Retrieve all categories.
  - GET /api/todo/category/{id}: Retrieve a specific category by ID.
  - POST /api/todo/category: Create a new category.
  - PUT /api/todo/category/{id}: Update an existing category by ID.
  - DELETE /api/todo/category/{id}: Delete a category by ID
- Item APIs:
  - GET /api/todo/category/{id}/item: Retrieve all items corresponding to a specific category.
  - GET /api/todo/category/{id}/item/{id}: Retrieve a specific item by ID corresponding to a specific category.
  - POST /api/todo/category/{id}/item: Create a new item corresponding to a specific category.
  - PUT /api/todo/category/{id}/item/{id}: Update an existing item by ID corresponding to a specific category.
  - DELETE /api/todo/category/{id}/item/{id}: Delete an item by ID corresponding to a specific category.
- Token API:
  - POST /api/auth: Obtain an authentication token by providing valid username and password.
- Admin API:
  - GET /admin: Retrieve all data (all user with all their categories/items lists) with administrative role permissions.

 ## Categories and Items Relationship
There is a personalized relationship between categories and items for each user. This means that every user has their own set of categories and items, ensuring that user data remains separate and private. when accessing **Category** or **Item** APIs, the **userId** is fetched from the token, allowing the retrieval of their specific data.
A category contains a list of items and includes the following properties:
```
{
        "id": "c4f1e429-bcfb-466a-81ea-6636b4931507",
        "name": "category1",
        "label": 1,
        "color": "red",
        "userId": "64c38a45e9288a2cf9a2b363",
        "isStarred": true,
        "createdDate": "2023-07-31T06:33:18.091Z",
        "updatedDate": "2023-07-31T06:33:20.103Z"
    }
```
On the other hand, each item is associated with a category and contains the following properties:


```
{
        "id": "6e77d320-d264-48ef-8fc8-49394e3a2a9c",
        "categoryId": "c4f1e429-bcfb-466a-81ea-6636b4931507",
        "name": "item-1-a",
        "content": "Don't forget to submit the assignment",
        "dueDate": "2023-07-02T21:00:00Z",
        "isDone": true,
        "createdDate": "2023-07-31T06:33:29.355Z",
        "updatedDate": "2023-07-31T06:33:31.351Z"
    }
```
By leveraging the userId associated with each operation, users gain control over their individual categories and items, ensuring a personalized and confidential experience within the ToDo project.

## Users and Roles
During startup, the project is initialized with preconfigured users and roles. There exist two distinct roles: **Admin** and **NormalUser**. Users are created for each role, including **admin_user**, **normal_user_1**, and **normal_user_2**. 
The admin role possesses the capability to retrieve data for all users by utilizing the Admin API. Conversely, normal users have access to the category and item APIs, which allows them to independently create and manage their own to-do lists.
