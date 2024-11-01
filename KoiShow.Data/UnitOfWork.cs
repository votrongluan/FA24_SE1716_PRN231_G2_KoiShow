using KoiShow.Common.DTO.DTORequest;
using KoiShow.Common.DTO.DtoResponse;
using KoiShow.Data.Models;
using KoiShow.Data.Repository;

namespace KoiShow.Data;

public class UnitOfWork
{
    private FA24_SE1716_PRN231_G2_KoiShowContext context;
    private ContestResultRepository contestResultRepository;
    private ContestRepository contestRepository;
    private AccountRepository accountRepository;
    private PointRepository pointRepository;
    private PaymentRepository paymentRepository;
    private RegisterFormRepository registerFormRepository;
    private AnimalRepository animalRepository;
    private VarietyRepository varietyRepository;

    public UnitOfWork()
    {
        context ??= new FA24_SE1716_PRN231_G2_KoiShowContext();
    }

    public ContestResultRepository ContestResultRepository
    {
        get
        {
            return contestResultRepository ??= new ContestResultRepository(context);
        }
    }

    public ContestRepository ContestRepository
    {
        get
        {
            return contestRepository ??= new ContestRepository(context);
        }
    }

    public AccountRepository AccountRepository
    {
        get
        {
            return accountRepository ??= new AccountRepository(context);
        }
    }

    public PointRepository PointRepository
    {
        get
        {
            return pointRepository ??= new PointRepository(context);
        }
    }

    public PaymentRepository PPaymentRepository
    {
        get { return paymentRepository ??= new PaymentRepository(context); }
    }

    public RegisterFormRepository RegisterFormRepository
    {
        get { return registerFormRepository ??= new RegisterFormRepository(context); }
    }

    public AnimalRepository AnimalRepository
    {
        get { return animalRepository ??= new AnimalRepository(context); }
    }

    public VarietyRepository VarietyRepository
    {
        get { return varietyRepository ??= new VarietyRepository(context); }
    }

    public async Task<List<PaymentDtoResponse>> GetAllPaymentsAsync()
    {
        return await PPaymentRepository.GetAllPaymentsAsync();
    }

    public async Task<PaymentDtoResponse> CreatePaymentAsync(PaymentDto paymentDto)
    {
        return await PPaymentRepository.CreatePaymentAsync(paymentDto);
    }

    public async Task<PaymentDtoResponse> FindPaymentByIdAsync(int paymentId)
    {
        return await PPaymentRepository.FindPaymentByIdAsync(paymentId);
    }

    public async Task<List<PaymentDtoResponse>> FindPaymentsByStringAsync(string searchString)
    {
        return await PPaymentRepository.FindPaymentsByStringAsync(searchString);
    }

    public async Task<List<PaymentDtoResponse>> FindPaymentsByCriteriaAsync(string transactionId, string description, string paymentStatus)
    {
        return await PPaymentRepository.FindPaymentsByCriteriaAsync(transactionId, description, paymentStatus);
    }

    public async Task<PaymentDtoResponse> UpdatePaymentStatusToPaidAsync(int paymentId)
    {
        return await PPaymentRepository.UpdatePaymentStatusToPaidAsync(paymentId);
    }

    public async Task<PaymentDtoResponse> CancelPaymentStatusAsync(int paymentId)
    {
        return await PPaymentRepository.CancelPaymentStatusAsync(paymentId);
    }
}