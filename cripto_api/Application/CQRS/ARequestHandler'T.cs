using MediatR;
using Shared.ApiResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS;

public abstract class ARequestHandler<TRequest, TResponse>
    : IRequestHandler<TRequest, TResponse>
    where TRequest : ARequest<TResponse>
{
    // 1) Handle metodu FINAL’dır (override edilemez), çünkü burada
    //    ortak validasyon, loglama vb. yapmak istiyoruz.
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        // a) Log request (İsterseniz loglama atlanabilir)
        await LogRequestAsync(request);

        // b) Validasyon
        var (isValid, errorMessage) = request.Validate();
        if (!isValid)
        {
            // ExceptionFactory kullandığınızı varsayıyoruz:
            throw ExceptionFactory.BadRequest(errorMessage ?? "Invalid request.");
        }

        // c) Asıl iş mantığını (business logic) alt sınıfa bırakıyoruz
        var response = await HandleRequestAsync(request, cancellationToken);

        // d) Log response (isterseniz)
        await LogResponseAsync(response);

        return response;
    }

    // 2) Alt sınıfın, asıl işi yapacağı soyut metod
    protected abstract Task<TResponse> HandleRequestAsync(TRequest request, CancellationToken cancellationToken);

    // 3) Loglama metodları (opsiyonel, override etmek isterseniz virtual yapabilirsiniz)
    protected virtual Task LogRequestAsync(TRequest request)
    {
        Console.WriteLine($"[Request Log] RequestId={request.RequestId}");
        return Task.CompletedTask;
    }

    protected virtual Task LogResponseAsync(TResponse response)
    {
        Console.WriteLine("[Response Log] Handled successfully.");
        return Task.CompletedTask;
    }
}
