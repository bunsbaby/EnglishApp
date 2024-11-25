import React from 'react';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import AdminLayout from "layouts/AdminLayout";
import Login from 'pages/Login/Login';
import Register from 'pages/Register/Register';
import Teacher from 'pages/Teacher/Teacher';
import Student from 'pages/Student/Student';
import Course from 'pages/Course/Course';
import Document from 'pages/Document/Document';
import Home from 'pages/Home/Home';
import Revenue from 'pages/Revenue/Revenue';
import CalendarCom from 'pages/Calendar/Calendar';
import { message } from 'antd';
const App = () => {
  const [messageApi, contextHolder] = message.useMessage();
    const router = createBrowserRouter([
        {
          path: "/",
          element: <AdminLayout />,
          children: [
            {
              path: "/",
              index: true,
              element: <Home />
            },
            {
              path: "/teacher",
              element: <Teacher />
            },
            {
              path: "/student",
              element: <Student />,
            },
            {
              path: "/course",
              element: <Course />
            },
            {
              path: "/document",
              element: <Document />
            },
            {
              path: "/revenue",
              element: <Revenue />
            },
            {
              path: "/calendar",
              element: <CalendarCom />
            }
          ]
        },
        {
          path: '/login',
          element:  <Login />,
        },
        {
          path: '/register',
          element: <Register />
        }
      ]);
    return <div className="App min-h-screen">
        {contextHolder}
        <RouterProvider router={router} />
    </div>;
}
export default App;
