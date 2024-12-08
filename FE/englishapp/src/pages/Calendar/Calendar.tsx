import React, { useEffect, useState } from 'react';
import type { Dayjs } from 'dayjs';
import type { BadgeProps, CalendarProps } from 'antd';
import { Badge, Calendar, Row, Col } from 'antd';
import axios from '../../common/baseAxios';
interface ICalendar {
  type: string,
  content: string,
  day: number,
  id: number,
  year: number,
  month: number
}
const css = `
  .events {
    margin: 0;
    padding: 0;
    list-style: none;
  }
  .events .ant-badge-status {
    width: 100%;
    overflow: hidden;
    font-size: 12px;
    white-space: nowrap;
    text-overflow: ellipsis;
  }
  .notes-month {
    font-size: 28px;
    text-align: center;
  }
  .notes-month section {
    font-size: 28px;
  }
  .ant-picker-calendar-mode-switch {
    display: none;
  }
`
const CalendarCom = () => {
  const [currentCalendars, setCurrentCalendar] = useState<Array<ICalendar>>(Array<ICalendar>);
  useEffect(() => {
    getListCourses();
  }, []);
  const getListCourses = (request: any = {}) => {
    axios.post(`Classes/GetCalendars`, request).then((res) => {
      if (res?.data?.status) {
        setCurrentCalendar(res?.data?.data);
      }
    })
  }
  const onPanelChange = (newValue: Dayjs) => {
    getListCourses({ chooseDate: newValue });
  };
  const getListData = (value: Dayjs) => {
    let listData: Array<ICalendar> = [];
    let day: number = value.date();
    let month: number = value.month() + 1;
    let year: number = value.year();
    listData = currentCalendars.filter((m) => { return m.day == day && m.month == month && m.year == year });
    return listData || [];
  };
  const dateCellRender = (value: Dayjs) => {
    const listData = getListData(value);
    return (
      <ul className="events">
        {listData.map((item) => (
          <li key={item.content}>
            <Badge status={`success` as BadgeProps['status']} text={item.content} />
          </li>
        ))}
      </ul>
    );
  };
  const cellRender: CalendarProps<Dayjs>['cellRender'] = (current, info) => {
    if (info.type === 'date') return dateCellRender(current);
    return info.originNode;
  };
  return <>
    <style>{css}</style>
    <Row>
      <Col span={24} style={{ fontWeight: 700, fontSize: '23px' }}>QUẢN LÝ LỊCH HỌC - LỊCH LÀM BÀI KIỂM TRA</Col>
    </Row>
    <Calendar cellRender={cellRender} onPanelChange={onPanelChange} />
  </>
}

export default CalendarCom;