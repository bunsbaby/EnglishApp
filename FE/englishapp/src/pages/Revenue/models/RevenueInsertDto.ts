interface RevenueInsertDto {
    bankAccount: string,
    fee: string,
    name: string,
    paymentDeadline: Date,
    phone: string,
    unit: number,
    status: number,
    feeType: number,
}
export default RevenueInsertDto;