import React, { useEffect, useState } from 'react';
import { Button, Modal, Form, Input, Select, message } from 'antd';
import axios from '../../common/baseAxios';
import CourseDto from 'pages/Course/models/CourseDto';
import StudentInsertDto from './models/StudentInsertDto';
import StudentDto from './models/StudentDto';
interface IAddStudentProps {
    open: boolean,
    closeForm: Function,
    curentStudent: StudentDto
}
interface IClassOptions {
    value: Number,
    label: String
};
interface IGenderOptions {
    value: Number,
    label: String
}
const EditModal: React.FC<IAddStudentProps> = (props: IAddStudentProps) => {
    const { open, closeForm, curentStudent } = props;
    const [classOptions, setClassOptions]  = useState<any>(Array<IClassOptions>);
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
    const onFinish = (input: StudentInsertDto) => {
        axios.put(`Students/${curentStudent?.id}`, input).then((res) => {
            if (res?.data.status === true) {
                message.success('Sửa học viên thành công.')
                closeForm(true);
            }
        })
    }
    useEffect(() => {
        getCourse();
    }, []);
    const getCourse = () => {
        axios.get(`Courses`).then((res) => {
            if (res.data.status) {
                let courses: Array<CourseDto> = res.data.data;
                let courseOptions: Array<IClassOptions> = new Array<IClassOptions>();
                courses.forEach((m) => {
                    let option: IClassOptions =  {
                        value: m.id,
                        label: m.name
                    };
                    courseOptions.push(option);
                })
                setClassOptions(courseOptions);
            }
        })
    }
    return (
        <>
            <Modal
                title="SỬA HỌC VIÊN"
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
                        ["name"]: curentStudent?.name,
                        ["genderId"]: curentStudent?.genderId,
                        ["email"]: curentStudent?.email,
                        ["phone"]: curentStudent?.phone,
                        ["address"]: curentStudent?.address,
                        ["classId"]: curentStudent?.classId
                    }}
                >
                    <Form.Item label="Họ & Tên" name="name" rules={[{ required: true, message: 'Vui lòng nhập họ & tên của học viên!' }]}>
                        <Input placeholder='Họ & Tên của học viên' />
                    </Form.Item>
                    <Form.Item name="genderId" label="Giới Tính" rules={[{ required: true, message: 'Vui lòng chọn giới tính học viên !' }]}>
                        <Select
                            placeholder="Chọn giới tính học viên"
                            allowClear
                            options={genderOptions}
                        >
                        </Select>
                    </Form.Item>
                    <Form.Item
                        label="Địa Chỉ E-mail"
                        name="email"
                        rules={[
                            { required: true, message: 'Vui lòng nhập địa chỉ E-mail của học viên!' },
                            { type: 'email', message: 'Định dạng E-mail không đúng!' }
                        ]}
                    >
                        <Input placeholder='Địa chỉ E-mail của học viên' />
                    </Form.Item>
                    <Form.Item
                        label="Số Điện Thoại"
                        name="phone"
                        rules={[{ required: true, message: 'Vui lòng nhập số điện thoại của học viên!' }]}
                    >
                        <Input placeholder='Số điện thoại của học viên' />
                    </Form.Item>
                    <Form.Item
                        label="Địa Chỉ"
                        name="address"
                        rules={[{ required: false }]}
                    >
                        <Input placeholder='Địa chỉ của học viên' />
                    </Form.Item>
                    <Form.Item name="classId" label="Lớp" rules={[{ required: true, message: 'Vui lòng chọn lớp học viên !' }]}>
                        <Select
                            placeholder="Chọn lớp học viên"
                            allowClear
                            options={classOptions}
                        >
                        </Select>
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
