import React, { useEffect, useState } from 'react';
import { Button, Modal, Form, Input, Select, message, DatePicker, InputNumber } from 'antd';
import RevenueInsertDto from './models/RevenueInsertDto';
import RevenueStatus from 'pages/Revenue/models/RevenueStatusEnum';
import FeeTypeEnum from 'pages/Revenue/models/FeeTypeEnum';
import axios from '../../common/baseAxios';
import type { DatePickerProps } from 'antd';
import RevenueDto from 'pages/Revenue/models/RevenueDto';
import dayjs from 'dayjs';
interface IAddRevenueProps {
    open: boolean,
    closeForm: Function,
    curentRevenue: RevenueDto
}
interface IRevenueStatusOptions {
    value: Number,
    label: String
};
interface IRevenueFeeTypeOptions {
    value: Number,
    label: String
}
const EditModal: React.FC<IAddRevenueProps> = (props: IAddRevenueProps) => {
    const { open, closeForm, curentRevenue } = props;
    const statusOptions: Array<IRevenueStatusOptions> = [
        {
            label: 'Đã thanh toán',
            value: RevenueStatus.Paid
        },
        {
            label: 'Chưa thanh toán',
            value: RevenueStatus.Unpaid
        }
    ]
    const feeTypeOptions: Array<IRevenueFeeTypeOptions> = [
        {
            label: 'Chi',
            value: FeeTypeEnum.Fee
        },
        {
            label: 'Thu',
            value: FeeTypeEnum.Expense
        }
    ]
    const onFinish = (input: RevenueInsertDto) => {
        axios.put(`Revenues/${curentRevenue?.id}`, input).then((res) => {
            if (res?.data.status === true) {
                message.success('Sửa hóa đơn thành công.')
                closeForm(true);
            }
        })
    }
    return (
        <>
            <Modal
                title="SỬA HÓA ĐƠN"
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
                        ["name"]: curentRevenue?.name,
                        ["fee"]: curentRevenue?.fee,
                        ["feeType"]: curentRevenue?.feeType,
                        ["unit"]: curentRevenue?.unit,
                        ["status"]: curentRevenue?.status,
                        ["bankAccount"]: curentRevenue?.bankAccount,
                        ["phone"]: curentRevenue?.phone,
                        ["paymentDeadline"]: dayjs(curentRevenue?.paymentDeadline)
                    }}
                >
                    <Form.Item label="Tên" name="name" rules={[{ required: true, message: 'Vui lòng nhập tên người thanh toán!' }]}>
                        <Input placeholder='Tên người thanh toán' />
                    </Form.Item>
                    <Form.Item label="Tên phí" name="fee" rules={[{ required: true, message: 'Vui lòng nhập tên phí!'}]}>
                        <Input placeholder='Tên phí' />
                    </Form.Item>
                    <Form.Item name="feeType" label="Loại phí" rules={[{ required: true, message: 'Vui lòng chọn loại phí !' }]}>
                        <Select
                            placeholder="Chọn loại phí"
                            allowClear
                            options={feeTypeOptions}
                        >
                        </Select>
                    </Form.Item>
                    <Form.Item label="Đơn vị" name="unit" rules={[{ required: true, message: 'Vui lòng nhập đơn vị!'}]}>
                        <InputNumber style={{width: '100%'}} placeholder='Nhập số tiền cần thanh toán'
                        />
                    </Form.Item>
                    <Form.Item name="status" label="Tình trạng" rules={[{ required: true, message: 'Vui lòng chọn tình trạng hóa đơn !' }]}>
                        <Select
                            placeholder="Chọn tình trạng hóa đơn"
                            allowClear
                            options={statusOptions}
                        >
                        </Select>
                    </Form.Item>
                    <Form.Item
                        label="Số tài khoản"
                        name="bankAccount"
                        rules={[
                            { required: true, message: 'Vui lòng nhập số tài khoản!' },
                        ]}
                    >
                        <Input placeholder='Số tài khoản' />
                    </Form.Item>
                    <Form.Item name="phone" label="Số điện thoại" rules={[{ required: true, message: 'Vui lòng nhập số điện thoại !' }]}>
                        <Input placeholder='Số điện thoại' />
                    </Form.Item>
                    <Form.Item name="paymentDeadline" label="Hạn trả" rules={[ { required: true, message: 'Vui lòng chọn hạn trả.' }]}>
                        <DatePicker />
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
