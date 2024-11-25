import { ConfigProvider } from 'antd';
import React, { Suspense } from 'react';
import ReactDOM from 'react-dom/client';
import 'antd/dist/reset.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import './styles/global-styles.scss'

const root = ReactDOM.createRoot(document.getElementById('root') as HTMLElement);
root.render(
  <Suspense fallback={<div>Loading....</div>}>
    <ConfigProvider theme={{hashed: false}}>
      <App />
    </ConfigProvider>
  </Suspense>
);

reportWebVitals();
