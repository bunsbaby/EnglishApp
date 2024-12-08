import React, { useState } from 'react';
import { useNavigate } from "react-router-dom";
import { Outlet } from 'react-router-dom';
import Logo from 'access/images/Logo.png'
import Avatar from 'access/images/headeravatar.png';
import {
    PieChartOutlined,
    LogoutOutlined,
    WalletOutlined,
    FileAddOutlined,
    ReadOutlined,
    UserOutlined,
    TeamOutlined,
    CalendarOutlined,
    HomeOutlined,
    ProjectOutlined
} from '@ant-design/icons';
import { Breadcrumb, Layout, Menu, theme } from 'antd';
const { Header, Content, Footer, Sider } = Layout;
const AdminLayout = () => {
    const [collapsed, setCollapsed] = useState(false);
    const navigate = useNavigate();
    const {
        token: { colorBgContainer },
    } = theme.useToken();
    return (
        <Layout style={{ minHeight: '100vh' }}>
            <Sider theme="light" collapsible collapsed={collapsed} onCollapse={(value) => setCollapsed(value)}>
                <div className="demo-logo-vertical">
                    <img className={'logo'} src={Logo} alt="" />
                </div>
                <Menu defaultSelectedKeys={['1']} mode="inline">
                    <Menu.Item key={1} onClick={() => navigate("/")}><HomeOutlined /> <span>Trang Chủ</span></Menu.Item>
                    <Menu.Item key={2} onClick={() => navigate("/course")}><ProjectOutlined /> <span>Khóa Học</span></Menu.Item>
                    <Menu.Item key={3} onClick={() => navigate("/teacher")}><UserOutlined /> <span>Giảng Viên</span></Menu.Item>
                    <Menu.Item key={4} onClick={() => navigate("/student")}><TeamOutlined /> <span>Học Viên</span></Menu.Item>
                    <Menu.Item key={9} onClick={() => navigate("/class")}><ReadOutlined /> <span>Lớp học</span></Menu.Item>
                    <Menu.Item key={5} onClick={() => navigate("/document")}><FileAddOutlined /> <span>Tài Liệu</span></Menu.Item>
                    <Menu.Item key={6} onClick={() => navigate("/revenue")}><WalletOutlined /> <span>Doanh Thu</span></Menu.Item>
                    <Menu.Item key={7} onClick={() => navigate("/calendar")}><CalendarOutlined /> <span>Lịch</span></Menu.Item>
                    <Menu.Item key={8} onClick={() => navigate("/login")}><LogoutOutlined /> <span>Đăng Xuất</span></Menu.Item>
                </Menu>
            </Sider>
            <Layout>
                <Header style={{ padding: 0, background: colorBgContainer, textAlign: 'right' }} >
                    <img src={Avatar} alt='' style={{ paddingRight: `17px`, width: `65px`, cursor: `pointer`}} />
                </Header>
                <Content style={{ margin: '16px 16px' }}>
                    <div style={{ padding: 24, minHeight: 360, background: colorBgContainer }}><Outlet /></div>
                </Content>
                <Footer style={{ textAlign: 'center' }}>English App© Created by Nguyen Tuan Minh</Footer>
            </Layout>
        </Layout>
    );
};

export default AdminLayout;
