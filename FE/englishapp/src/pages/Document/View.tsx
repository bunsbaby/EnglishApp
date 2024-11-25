import React, { useState, useEffect } from 'react';
import { Col, Modal, Row, Button, message } from 'antd';
import DocumentDto from './models/DocumentDto';
import axios from '../../common/baseAxios';
import moment from 'moment'
interface IAddDocumentProps {
    open: boolean,
    closeForm: Function,
    id: Number
}
const ViewModal: React.FC<IAddDocumentProps> = (props: IAddDocumentProps) => {
    const { open, closeForm, id } = props;
    const [curentDocument, setCurentDocument] = useState<DocumentDto>();
    useEffect(() => {
        getDocumentById();
    }, [])
    const getDocumentById = () => {
        axios.get(`Documents/${id}`).then((res) => {
            setCurentDocument(res.data.data);
        })
    }
    const handleDelete = () => {
        axios.delete(`Documents/${id}`).then((res) => {
            if(res?.data?.status) {
                message.success('Xóa tài liệu thành công.')
                closeForm(true);
            }
            else {
                message.success('Xóa tài liệu thất bại.')
            }
        })
    }
    return (
        <>
            <Modal
                title="CHI TIẾT TÀI LIỆU"
                centered
                open={open}
                onOk={() => closeForm(false)}
                onCancel={() => closeForm(false)}
                footer={null}
                width={1000}
            >
                <Row>
                    <Col span={16}>
                        <Row style={{marginTop: '15px'}}>
                            <Col span={4}>Mã ID:</Col>
                            <Col span={20}>{curentDocument?.id}</Col>
                        </Row>
                        <Row style={{marginTop: '15px'}}>
                            <Col span={4}>Tên Tài Liệu:</Col>
                            <Col span={20}>{curentDocument?.name}</Col>
                        </Row>
                        <Row style={{marginTop: '15px'}}>
                            <Col span={4}>Ngày Tải Lên:</Col>
                            <Col span={20}>{moment(curentDocument?.createdAt).format("DD/MM/YYYY hh:mm")}</Col>
                        </Row>
                        <Row style={{marginTop: '15px'}}>
                            <Col span={4}>Kích Thước:</Col>
                            <Col span={20}>{ curentDocument === null || curentDocument === undefined ? '0MB' : `${(curentDocument.documentSize / (1024 ** 2)).toFixed(2)}MB`}</Col>
                        </Row>
                        <Row style={{marginTop: '15px'}}>
                            <Col span={4}>Mô Tả:</Col>
                            <Col span={20}>{curentDocument?.description}</Col>
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
