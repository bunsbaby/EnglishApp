import React from 'react';
import { Col, Row, theme, Form, Input, Button, message } from 'antd';
import LogoLogin from 'access/images/7522174.png';
import Logo from 'access/images/Logo.png';
import { PageWrapper } from 'pages/Login/Login.styled';
import { useNavigate } from "react-router-dom";
import LoginDto from './models/LoginDto';
import axios from '../../common/baseAxios';
import { Cookies } from "react-cookie";
let cookie = new Cookies();
const Login = () => {
    const navigate = useNavigate();
    const onFinish = (input: LoginDto) => {
        axios.post(`Accounts/Login`, input).then((result) => {
            if(result?.data?.status) {
                let expires = new Date();
                expires.setTime(expires.getTime() + 604800);
                cookie.set('token', result.data.data.token, { path: "/", expires: expires });
                axios.defaults.headers.common = { Authorization: `Bearer ${result.data.data.token}` };
                navigate("/");
                message.success('Đăng nhập thành công.')
            }
            else {
                message.error('Đăng nhập thất bại.')
            }
        })
    }
    return <PageWrapper>
        <Row gutter={[0, 0]} className={'row-1'}>
            <Col span={12} className={'box-left'}>
                <img className={'bg-1'} src={LogoLogin} alt="" />
                <img className={'bg-2'} src={LogoLogin} alt="" />
            </Col>
            <Col span={10} className={'content-login'}>
                <div>
                    <img className={'logo'} src={Logo} alt="" />
                    <h2 className='title-login'>ĐĂNG NHẬP</h2>
                    <Form
                        name="basic"
                        layout={'vertical'}
                        style={{ maxWidth: 600 }}
                        onFinish={onFinish}
                        autoComplete="off"
                    >
                        <Form.Item
                            label="Tên đăng nhập:"
                            name="username"
                            rules={[{ required: true, message: 'Vui lòng nhập tên đăng nhập của bạn!' }]}
                        >
                            <Input placeholder='Tên đăng nhập của bạn' />
                        </Form.Item>

                        <Form.Item
                            label="Mật khẩu"
                            name="password"
                            rules={[{ required: true, message: 'Vui lòng nhập mật khẩu của bạn!' }]}
                        >
                            <Input.Password placeholder='Mật khẩu của bạn' />
                        </Form.Item>

                        <Form.Item wrapperCol={{ span: 24 }}>
                            <Button type="primary" htmlType="submit" style={{ width: '100%' }}>
                                ĐĂNG NHẬP
                            </Button>
                        </Form.Item>
                        <Form.Item wrapperCol={{ span: 24 }} style={{ fontWeight: 400, fontSize: '16px', display: 'flex', justifyContent: 'center'}}>
                            <span style={{ color: '#7D7D7D' }}>Bạn chưa có tài khoản ? </span> <span onClick={() => navigate("/register")} style={{ color: '#000000', cursor: 'pointer' }}>Đăng Ký</span>
                        </Form.Item>
                    </Form>
                </div>
            </Col>
        </Row>
    </PageWrapper>
}
export default Login;