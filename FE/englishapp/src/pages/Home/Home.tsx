import React, { useEffect, useState } from "react";
import { Button, Col, Row, Input, Space, Table, Tag, Card } from "antd";
import { SearchOutlined, InfoCircleOutlined } from '@ant-design/icons';
import Group3 from 'access/images/Group3.png';
import Group2 from 'access/images/Group2.png';
import { Line } from '@ant-design/charts';
import axios from '../../common/baseAxios';
import { Cookies } from "react-cookie";
import { BarChart } from '@mui/x-charts/BarChart';
import { axisClasses } from '@mui/x-charts';
import { Chart } from 'react-google-charts';
let cookie = new Cookies();
const jwtToken = cookie.get(`token`);
if (jwtToken !== null) {
    axios.defaults.headers.common = { Authorization: `Bearer ${jwtToken}` };
}
interface IDashboardDto {
    totalTeacher: number,
    totalStudent: number,
}
interface IBarChartDto {
    month: string,
    fee: number,
    expense: number
}
const Home = () => {
    const [dataDashboard, setData] = useState<IDashboardDto>();
    const [config, setConfig] = useState({
        height: 400,
        xField: 'month',
        yField: 'value',
        point: {
            size: 5,
            shape: 'diamond | circule',
        },
        data: []
    });
    const [barCharts, setBarCharts] = useState([
        ['Month', 'Thu', 'Chi'],
        ['Jan', 0, 0],
        [`Feb`, 0, 0],
        [`Mar`, 0, 0],
        [`Apr`, 0, 0],
        [`May`, 0, 0],
        [`Jun`, 0, 0],
        [`Jul`, 0, 0],
        [`Aug`, 0, 0],
        [`Sep`, 0, 0],
        [`Oct`, 0, 0],
        [`Nov`, 0, 0],
        [`Dec`, 0, 0],
    ])
    const chartSetting = {
        height: 300,
    };
    useEffect(() => {
        getDashboards();
    }, []);
    const getDashboards = () => {
        axios.get(`Dashboards`).then((result) => {
            if (result?.data?.data) {
                if (result.data.data.barCharts) {
                    let resultBarCharts: Array<IBarChartDto> = result.data.data.barCharts;
                    for (var i in barCharts) {
                        let barChart = resultBarCharts.find(m => m.month == barCharts[i][0]);
                        if (barChart) {
                            barCharts[i][1] = barChart.expense;
                            barCharts[i][2] = barChart.fee;
                        }
                    }
                }
                setData(result.data.data)
                setConfig({ ...config, data: result.data.data.dashboardCharts })
            }
        })
    }
    return <div>
        <Row>
            <Col span={24} style={{ fontWeight: 700, fontSize: '23px' }}>TRANG CHỦ</Col>
        </Row>
        <Row style={{ marginTop: '20px' }}>
            <Col span={6}>
                <Row>
                    <Col span={24}>
                        THỐNG KÊ
                    </Col>
                </Row>
                <Row style={{ marginTop: '20px' }}>
                    <Col span={24}>
                        <Card>
                            <Row>
                                <Col span={6} style={{ borderRight: '1px solid #D60A0B', display: 'flex', justifyContent: 'center', alignItems: 'center' }}><img style={{ width: '70%' }} src={Group3} alt="" /></Col>
                                <Col span={18} style={{ paddingLeft: '10px' }}>
                                    <h3 style={{ color: '#A3A3A3', fontSize: '14px', fontWeight: 400 }}>Giảng Viên</h3>
                                    <h3 style={{ fontWeight: 600, fontSize: '20px' }}>{dataDashboard?.totalTeacher}</h3>
                                </Col>
                            </Row>
                        </Card>
                    </Col>
                </Row>
                <Row style={{ marginTop: '20px' }}>
                    <Col span={24}>
                        <Card>
                            <Row>
                                <Col span={6} style={{ borderRight: '1px solid #D60A0B', display: 'flex', justifyContent: 'center', alignItems: 'center' }}><img style={{ width: '70%' }} src={Group2} alt="" /></Col>
                                <Col span={18} style={{ paddingLeft: '10px' }}>
                                    <h3 style={{ color: '#A3A3A3', fontSize: '14px', fontWeight: 400 }}>Học Viên</h3>
                                    <h3 style={{ fontWeight: 600, fontSize: '20px' }}>{dataDashboard?.totalStudent}</h3>
                                </Col>
                            </Row>
                        </Card>
                    </Col>
                </Row>
            </Col>
            <Col span={2}></Col>
            <Col span={16}>
                <Row>
                    <Col span={24}>
                        THỐNG KÊ
                    </Col>
                </Row>
                <Row style={{ marginTop: '20px' }}>
                    <Col span={24}>
                        <Line {...config} />
                    </Col>
                </Row>

            </Col>
        </Row>

        <Row style={{ marginTop: '20px' }}>
            <Col span={24}>
                DOANH THU
            </Col>
        </Row>
        <Row style={{ marginTop: '20px' }}>
            <Col span={24}>
                <Chart
                    chartType="ColumnChart"
                    data={barCharts}
                    width="100%"
                    height="400px"
                    legendToggle
                />
            </Col>
        </Row>
    </div>
}
export default Home;