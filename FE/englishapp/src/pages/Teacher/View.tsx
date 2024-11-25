import React, { useEffect, useState } from 'react';
import { Col, Modal, Row, Space, message, Button } from 'antd';
import DefaultAvatar from 'access/images/avatar.png';
import axios from '../../common/baseAxios';
import TeacherDto from 'pages/Teacher/models/TeacherDto';
interface IAddTeacherProps {
    open: boolean,
    closeForm: Function,
    id: Number
}
const ViewModal: React.FC<IAddTeacherProps> = (props: IAddTeacherProps) => {
    const { open, closeForm, id } = props;
    const [curentTeacher, setCurentTeacher] = useState<TeacherDto>();

    useEffect(() => {
        getTeacherById();
    }, [])
    const getTeacherById = () => {
        axios.get(`Teachers/${id}`).then((res) => {
            setCurentTeacher(res.data.data);
        })
    }
    const handleDelete = () => {
        axios.delete(`Teachers/${id}`).then((res) => {
            if(res?.data?.status) {
                message.success('Xóa giảng viên thành công.')
                closeForm(true);
            }
            else {
                message.success('Xóa giảng viên thất bại.')
            }
        })
    }
    return (
        <>
            <Modal
                title="CHI TIẾT GIẢNG VIÊN"
                centered
                open={open}
                onOk={() => closeForm(false)}
                onCancel={() => closeForm(false)}
                footer={null}
                width={1000}
            >
                <Row>
                    <Col span={8}>
                        <img src={DefaultAvatar} alt='' />
                    </Col>
                    <Col span={16}>
                        <Row>
                            <Col span={24} style={{ fontSize: '32px', fontWeight: 700 }}>
                                {curentTeacher?.name}
                            </Col>
                        </Row>
                        <Row>
                            <Col span={24} style={{ fontSize: '16px', fontWeight: 400 }}>
                                Mô tả trình độ, học vấn: {curentTeacher?.education}
                            </Col>
                        </Row>
                        <Row style={{ marginTop: '15px' }}>
                            <Col span={4}>Mã ID:</Col>
                            <Col span={20}>{curentTeacher?.id?.toString()}</Col>
                        </Row>
                        <Row style={{ marginTop: '15px' }}>
                            <Col span={4}>E-mail:</Col>
                            <Col span={20}>{curentTeacher?.email}</Col>
                        </Row>
                        <Row style={{ marginTop: '15px' }}>
                            <Col span={4}>Giới tính:</Col>
                            <Col span={20}>
                                <Space size="middle">
                                    {curentTeacher?.genderId === 1 ? 'Nam' : (curentTeacher?.genderId === 2 ? 'Nữ' : 'Khác')}
                                </Space>
                            </Col>
                        </Row>
                        <Row style={{ marginTop: '15px' }}>
                            <Col span={4}>Địa chỉ:</Col>
                            <Col span={20}>{curentTeacher?.address}</Col>
                        </Row>
                        <Row style={{ marginTop: '15px' }}>
                            <Col span={4}>Ngày làm việc:</Col>
                            <Col span={20}></Col>
                        </Row>
                    </Col>
                </Row>
                <Row style={{ marginTop: '15px', textAlign: 'right' }}>
                    <Col span={24}>
                        <Button type="primary" danger onClick={() => handleDelete()}>Xóa</Button>
                    </Col>
                </Row>
            </Modal>
        </>
    );
};

export default ViewModal;
