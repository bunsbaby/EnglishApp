import React, { useState } from 'react';
import { Button, Modal, Form, Input, Select, message } from 'antd';
import TeacherInsertDto from './models/TeacherInsertDto';
import axios from '../../common/baseAxios';
import TeacherDto from './models/TeacherDto';
interface IAddTeacherProps {
    open: boolean,
    closeForm: Function,
    curentTeacher: TeacherDto
}
interface IGenderOptions {
    value: Number,
    label: String
}
const EditModal: React.FC<IAddTeacherProps> = (props: IAddTeacherProps) => {
    const { open, closeForm, curentTeacher } = props;
    const genderOptions: Array<IGenderOptions> = [
        {
            label: 'Nam',
            value: 1
        },
        {
            label: 'Nữ',
            value: 2
        },
        {
            label: 'Khác',
            value: 3
        }
    ]
    const onFinish = (input: TeacherInsertDto) => {
        axios.put(`Teachers/${curentTeacher.id}`, input).then((res) => {
            if (res?.data.status === true) {
                message.success('Sửa giảng viên thành công.')
                closeForm(true);
            }
        })
    }
    return (
        <>
            <Modal
                title="SỬA GIẢNG VIÊN"
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
                    initialValues={{
                        ["name"]: curentTeacher?.name,
                        ["genderId"]: curentTeacher?.genderId,
                        ["email"]: curentTeacher?.email,
                        ["phone"]: curentTeacher?.phone,
                        ["address"]: curentTeacher?.address,
                        ["education"]: curentTeacher?.education
                    }}
                >
                    <Form.Item label="Họ & Tên" name="name" rules={[{ required: true, message: 'Vui lòng nhập họ & tên của giảng viên!' }]}>
                        <Input placeholder='Họ & Tên của giảng viên' />
                    </Form.Item>
                    <Form.Item name="genderId" label="Giới Tính" rules={[{ required: true, message: 'Vui lòng chọn giới tính giảng viên !' }]}>
                        <Select
                            placeholder="Chọn giới tính giảng viên"
                            allowClear
                            options={genderOptions}
                        >
                        </Select>
                    </Form.Item>
                    <Form.Item
                        label="Địa Chỉ E-mail"
                        name="email"
                        rules={[
                            { required: true, message: 'Vui lòng nhập địa chỉ E-mail của giảng viên!' },
                            { type: 'email', message: 'Định dạng E-mail không đúng!' }
                        ]}
                    >
                        <Input placeholder='Địa chỉ E-mail của giảng viên' />
                    </Form.Item>
                    <Form.Item
                        label="Số Điện Thoại"
                        name="phone"
                        rules={[{ required: true, message: 'Vui lòng nhập số điện thoại của giảng viên!' }]}
                    >
                        <Input placeholder='Số điện thoại của giảng viên' />
                    </Form.Item>
                    <Form.Item
                        label="Trình Độ Học Vấn"
                        name="education"
                        rules={[{ required: false }]}
                    >
                        <Input placeholder='Trình độ học vấn của giảng viên' />
                    </Form.Item>
                    <Form.Item
                        label="Địa Chỉ"
                        name="address"
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

export default EditModal;
