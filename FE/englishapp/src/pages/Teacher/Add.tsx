import React, { useState } from 'react';
import { Button, Modal, Form, Input, Select, message } from 'antd';
import TeacherInsertDto from './models/TeacherInsertDto';
import axios from '../../common/baseAxios';
interface IAddTeacherProps {
    open: boolean,
    closeForm: Function,
}
const AddModal: React.FC<IAddTeacherProps> = (props: IAddTeacherProps) => {
    const { open, closeForm } = props;
    const onFinish = (input: TeacherInsertDto) => {
        axios.post(`Teachers`, input).then((res) => {
            if (res?.data.status === true) {
                message.success('Tạo giảng viên thành công.')
                closeForm(true);
            }
        })
    }
    return (
        <>
            <Modal
                title="THÊM GIẢNG VIÊN"
                centered
                open={open}
                onOk={() => closeForm(false)}
                onCancel={() => closeForm(false)}
                footer={null}
                width={1000}
            >
                <Form
                    name="basic"
                    layout={'vertical'}
                    style={{ maxWidth: 1000 }}
                    onFinish={onFinish}
                    autoComplete="off"
                >
                    <Form.Item label="Họ & Tên" name="Name" rules={[{ required: true, message: 'Vui lòng nhập họ & tên của giảng viên!' }]}>
                        <Input placeholder='Họ & Tên của giảng viên' />
                    </Form.Item>
                    <Form.Item name="GenderId" label="Giới Tính" rules={[{ required: true, message: 'Vui lòng chọn giới tính giảng viên !' }]}>
                        <Select
                            placeholder="Chọn giới tính giảng viên"
                            allowClear
                        >
                            <Select.Option value="1">Nam</Select.Option>
                            <Select.Option value="2">Nữ</Select.Option>
                            <Select.Option value="3">Khác</Select.Option>
                        </Select>
                    </Form.Item>
                    <Form.Item
                        label="Địa Chỉ E-mail"
                        name="Email"
                        rules={[
                            { required: true, message: 'Vui lòng nhập địa chỉ E-mail của giảng viên!' },
                            { type: 'email', message: 'Định dạng E-mail không đúng!' }
                        ]}
                    >
                        <Input placeholder='Địa chỉ E-mail của giảng viên' />
                    </Form.Item>
                    <Form.Item
                        label="Số Điện Thoại"
                        name="Phone"
                        rules={[{ required: true, message: 'Vui lòng nhập số điện thoại của giảng viên!' }]}
                    >
                        <Input placeholder='Số điện thoại của giảng viên' />
                    </Form.Item>
                    <Form.Item
                        label="Trình Độ Học Vấn"
                        name="Education"
                        rules={[{ required: false }]}
                    >
                        <Input placeholder='Trình độ học vấn của giảng viên' />
                    </Form.Item>
                    <Form.Item
                        label="Địa Chỉ"
                        name="Address"
                        rules={[{ required: false }]}
                    >
                        <Input placeholder='Địa chỉ của giảng viên' />
                    </Form.Item>

                    <Form.Item wrapperCol={{ span: 24 }} style={{ textAlign: 'right' }}>
                        <Button type="primary" htmlType="submit" >
                            Lưu
                        </Button>
                    </Form.Item>
                </Form>
            </Modal>
        </>
    );
};

export default AddModal;
