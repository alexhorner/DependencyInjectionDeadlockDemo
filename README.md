# Dependency Injection Deadlock Demo

The tests in this project demonstrate a minimal code example for reproducing a deadlock bug first encountered in a production application.

It appears this deadlock occurs as a result of an `await`ed call, which can be seen by debugging the `DeadlockOnScoped` test.

The `NoDeadlockOnSingleton` and `NoDeadlockOnTransient` tests are included as a demonstration that the DI deadlock bug only occurs on scoped services.

A scope is created regardless of the test, because this more closely replicates the production environment (ASP.NET) in which there is always a scope created (HTTP request scope).

---

# RESOLVED - Known Bug - nofix
https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection-guidelines#async-di-factories-can-cause-deadlocks

---

`CacheBasedDeadlockDemoTests` shows an ideal fix for this issue.

`WorkaroundDeadlockDemoTests` shows a less than ideal workaround for this issue.