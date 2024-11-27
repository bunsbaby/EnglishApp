import React, { useEffect, useState } from 'react';
import { Button, Modal, Form, Input, Select, message, DatePicker } from 'antd';
import CourseInsertDto from './models/CourseInsertDto';
import axios from '../../common/baseAxios';
import PackageCourseType from 'pages/Course/models/PackageCourseType';
interface IAddCourseProps {
    open: boolean,
    closeForm: Function,
}
interface IOptions {
    value: Number,
    label: String
};
const AddModal: React.FC<IAddCourseProps> = (props: IAddCourseProps) => {
    const { open, closeForm } = props;
    const packageCourse: Array<IOptions> = [
        {
            label: `1 tháng`,
            value: PackageCourseType.OneMonth
        },
        {
            label: `2 tháng`,
            value: PackageCourseType.TwoMonth
        },
        {
            label: `3 tháng`,
            value: PackageCourseType.ThreeMonth
        },
        {
            label: `4 tháng`,
            value: PackageCourseType.FourMonth
        },
        {
            label: `5 tháng`,
            value: PackageCourseType.FiveMonth
        },
        {
            label: `6 tháng`,
            value: PackageCourseType.SixMonth
        },
        { 
            label: `9 tháng`,
            value: PackageCourseType.NineMonth
        },
        { 
            label: `12 tháng`,
            value: PackageCourseType.OneYear
        }
    ];
    const onFinish = (input: CourseInsertDto) => {
        axios.post(`Courses`, input).then((res) => {
            if (res?.data.status === true) {
                message.success('Tạo khóa học thành công.')
                closeForm(true);
            }
        })
    }
    return (
        <>
            <Modal
                title="THÊM KHÓA HỌC"
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
                    <Form.Item label="Khóa học" name="name" rules={[{ required: true, message: 'Vui lòng nhập khóa học!' }]}>
                        <Input placeholder='Tên khóa học' />
                    </Form.Item>
                    <Form.Item label="Mô tả Khóa học" name="description" rules={[{ message: 'Vui lòng nhập mô tả!' }]}>
                        <Input placeholder='Mô tả khóa học' />
                    </Form.Item>
                    <Form.Item
                        label="Thời gian bắt đầu"
                        name="startDated"
                        rules={[
                            { required: true, message: 'Vui lòng nhập thời gian bắt đầu khóa học!' },
                        ]}
                    >
                         <DatePicker placeholder='Thời gian bắt đầu khóa học' />
                    </Form.Item>
                    <Form.Item
                        label="Gói khóa học"
                        name="packageType"
                        rules={[
                            { required: true, message: 'Vui lòng chọn gói khóa học!' },
                        ]}
                    >
                        <Select
                            placeholder="Chọn gói khóa học"
                            allowClear
                            options={packageCourse}
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

export default AddModal;
