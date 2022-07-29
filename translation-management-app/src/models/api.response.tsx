export default class ApiResponse {
    public IsSuccess: boolean;
    public Data: any;
    public Message: string;    

    constructor(isSuccess: boolean, data: any, mess: string) {
        this.IsSuccess = isSuccess;
        this.Data = data;
        this.Message = mess;        
    }
}